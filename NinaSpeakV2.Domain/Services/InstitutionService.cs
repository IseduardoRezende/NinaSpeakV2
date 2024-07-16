using AutoMapper;
using NinaSpeakV2.Data.Models;
using NinaSpeakV2.Data.Repositories.IRepositories;
using NinaSpeakV2.Domain.Entities;
using NinaSpeakV2.Domain.Extensions;
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

        public override async Task<ReadInstitutionViewModel> CreateAsync(CreateInstitutionViewModel createModel)
        {            
            if (!BaseValidator.IsAbove(createModel.UserFk, BaseValidator.IdMinValue))
                return new ReadInstitutionViewModel { BaseErrors = [new BaseError(BaseError.InvalidValue)] };

            var model = await base.CreateAsync(createModel);

            if (model.HasErrors())
                return model;

            var userInstitution = new CreateUserInstitutionViewModel 
            { 
                InstitutionFk = (long)model.Id!, 
                UserFk = createModel.UserFk,
                Owner = true,
                Writer = true,
                Creator = true,
            };

            var userInstitutionModel = await _userInstitutionService.CreateAsync(userInstitution);   
            
            if (userInstitutionModel.HasErrors())            
                return new ReadInstitutionViewModel { BaseErrors = userInstitutionModel.BaseErrors };            
            
            return model;
        }

        protected override Func<Institution, bool> ApplyFilters()
        {            
            return _ => true;
        }

        protected override async Task<IEnumerable<BaseError>> ValidateCreationAsync(CreateInstitutionViewModel createModel)
        {
            var errors = new List<BaseError>();

            if (!BaseValidator.IsValid(createModel))
            {
                errors.Add(new BaseError(BaseError.NullObject));
                return errors;
            }

            if (!InstitutionValidator.IsValidName(createModel.Name))
                errors.Add(new BaseError(BaseError.InvalidName));

            if (!BaseEnumValidator.IsValidDescription(createModel.Description))
                errors.Add(new BaseError(BaseError.InvalidDescription));

            if (!InstitutionValidator.IsValidImage(createModel.Image))
                errors.Add(new BaseError(BaseError.InvalidImage));

            if (await _institutionRepository.NameAlreadyExistAsync(createModel.Name))
                errors.Add(new BaseError(BaseError.NameAlreadyExist));

            return errors;
        }

        protected override async Task<IEnumerable<BaseError>> ValidateChangeAsync(UpdateInstitutionViewModel updateModel)
        {
            var errors = new List<BaseError>();

            if (!BaseValidator.IsValid(updateModel))
            {
                errors.Add(new BaseError(BaseError.NullObject));
                return errors;
            }

            if (!InstitutionValidator.IsValidName(updateModel.Name))
                errors.Add(new BaseError(BaseError.InvalidName));
            
            if (!BaseEnumValidator.IsValidDescription(updateModel.Description))
                errors.Add(new BaseError(BaseError.InvalidDescription));

            if (!InstitutionValidator.IsValidImage(updateModel.Image))
                errors.Add(new BaseError(BaseError.InvalidImage));

            var model = await _institutionRepository.GetByIdAsync(updateModel.Id);

            if (!BaseValidator.IsValid(model))
            {
                errors.Add(new BaseError(BaseError.InstitutionNotFound));
                return errors;
            }

            if (InstitutionValidator.IsEqual(model!, updateModel)) //Create an abstract implementation (IsEqual) foreach [Model]Validator
                errors.Add(new BaseError(BaseError.NoChangesDetected));

            if (!await _institutionRepository.CanChangeNameAsync(model!, updateModel.Name))
                errors.Add(new BaseError(BaseError.NameAlreadyExist));

            return errors;
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

        protected override void UpdateFields(Institution model, UpdateInstitutionViewModel updateModel)
        {
            model.Name = updateModel.Name;
            model.Description = updateModel.Description;
            model.Image = updateModel.FileName;
        }
    }
}
