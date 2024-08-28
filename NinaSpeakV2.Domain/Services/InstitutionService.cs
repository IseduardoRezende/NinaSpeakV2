using AutoMapper;
using NinaSpeakV2.Data.Entities;
using NinaSpeakV2.Data.Repositories.IRepositories;
using NinaSpeakV2.Domain.Extensions;
using NinaSpeakV2.Domain.Models;
using NinaSpeakV2.Domain.Services.IServices;
using NinaSpeakV2.Domain.Validators;
using NinaSpeakV2.Domain.ViewModels.Institutions;
using NinaSpeakV2.Domain.ViewModels.UsersInstitutions;

namespace NinaSpeakV2.Domain.Services
{
    public class InstitutionService : BaseService<Institution, CreateInstitutionViewModel, UpdateInstitutionViewModel, ReadInstitutionViewModel>,
                                      IInstitutionService
    {
        private readonly IInstitutionRepository _institutionRepository;
        private readonly IUserInstitutionService _userInstitutionService;

        public InstitutionService(IInstitutionRepository institutionRepository, IUserInstitutionService userInstitutionService,
                                  IMapper mapper) : base(institutionRepository, mapper)
        { 
            _institutionRepository = institutionRepository;
            _userInstitutionService = userInstitutionService;
        }

        //TODO: IMAGE HANDLER

        public override async Task<ReadInstitutionViewModel> CreateAsync(CreateInstitutionViewModel createViewModel)
        {            
            if (!BaseValidator.IsAbove(createViewModel.UserFk, BaseValidator.IdMinValue))
                return new ReadInstitutionViewModel { BaseErrors = [new BaseError(BaseError.InvalidValue)] };

            var entity = await base.CreateAsync(createViewModel);

            if (entity.HasErrors())
                return entity;

            var userInstitution = new CreateUserInstitutionViewModel 
            { 
                InstitutionFk = (long)entity.Id!, 
                UserFk = createViewModel.UserFk,
                Owner = true,
                Creator = true,
            };

            var userInstitutionModel = await _userInstitutionService.CreateAsync(userInstitution);   
            
            if (userInstitutionModel.HasErrors())            
                return new ReadInstitutionViewModel { BaseErrors = userInstitutionModel.BaseErrors };            
            
            return entity;
        }

        protected override Func<Institution, bool> ApplyFilters()
        {            
            return _ => true;
        }

        protected override async Task<IEnumerable<BaseError>> ValidateCreationAsync(CreateInstitutionViewModel createViewModel)
        {
            var errors = new List<BaseError>();

            if (!BaseValidator.IsValid(createViewModel))
            {
                errors.Add(new BaseError(BaseError.NullObject));
                return errors;
            }

            if (!InstitutionValidator.IsValidName(createViewModel.Name))
                errors.Add(new BaseError(BaseError.InvalidName));

            if (!BaseEnumValidator.IsValidDescription(createViewModel.Description))
                errors.Add(new BaseError(BaseError.InvalidDescription));

            if (!InstitutionValidator.IsValidImage(createViewModel.Image))
                errors.Add(new BaseError(BaseError.InvalidImage));

            if (await _institutionRepository.NameAlreadyExistAsync(createViewModel.Name))
                errors.Add(new BaseError(BaseError.NameAlreadyExist));

            return errors;
        }

        protected override async Task<IEnumerable<BaseError>> ValidateChangeAsync(UpdateInstitutionViewModel updateViewModel)
        {
            var errors = new List<BaseError>();

            if (!BaseValidator.IsValid(updateViewModel))
            {
                errors.Add(new BaseError(BaseError.NullObject));
                return errors;
            }

            if (!InstitutionValidator.IsValidName(updateViewModel.Name))
                errors.Add(new BaseError(BaseError.InvalidName));
            
            if (!BaseEnumValidator.IsValidDescription(updateViewModel.Description))
                errors.Add(new BaseError(BaseError.InvalidDescription));

            if (!InstitutionValidator.IsValidImage(updateViewModel.Image))
                errors.Add(new BaseError(BaseError.InvalidImage));

            var entity = await _institutionRepository.GetByIdAsync(updateViewModel.Id);

            if (!BaseValidator.IsValid(entity))
            {
                errors.Add(new BaseError(BaseError.InstitutionNotFound));
                return errors;
            }

            if (InstitutionValidator.IsEqual(entity!, updateViewModel)) //Create an abstract implementation (IsEqual) foreach [Model]Validator
                errors.Add(new BaseError(BaseError.NoChangesDetected));

            if (!await _institutionRepository.CanChangeNameAsync(entity!, updateViewModel.Name))
                errors.Add(new BaseError(BaseError.NameAlreadyExist));

            return errors;
        }

        public async Task<ReadInstitutionViewModel> GetStandardAsync()
        {
            var standardInstitution = await _institutionRepository.GetStandardAsync();

            ArgumentNullException.ThrowIfNull(standardInstitution, nameof(standardInstitution));
            return _mapper.Map<ReadInstitutionViewModel>(standardInstitution);
        }

        public override async Task<bool> SoftDeleteAsync(long id)
        {
            if (!await base.SoftDeleteAsync(id))
                return false;

            if (!await _userInstitutionService.SoftDeleteByInstitutionFkAsync(id))
            {
                await base.ActiveAsync(id);
                return false;
            }
            
            return true;
        }

        public bool TryGetByCode(string? code, out ReadInstitutionViewModel? institution)
        {
            institution = default;            

            if (!InstitutionValidator.IsValidCode(code))
                return false;

            institution = base.GetByAsync(i => i.Code == code).GetAwaiter().GetResult();
            return institution is not null;
        }

        protected override void UpdateFields(Institution entity, UpdateInstitutionViewModel updateViewModel)
        {
            entity.Name = updateViewModel.Name;
            entity.Description = updateViewModel.Description;
            entity.Image = updateViewModel.FileName;
        }
    }
}
