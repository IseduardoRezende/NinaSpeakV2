using AutoMapper;
using NinaSpeakV2.Data.Models;
using NinaSpeakV2.Data.Repositories.IRepositories;
using NinaSpeakV2.Domain.Entities;
using NinaSpeakV2.Domain.Services.IServices;
using NinaSpeakV2.Domain.Validators;
using NinaSpeakV2.Domain.ViewModels.UsersInstitutions;

namespace NinaSpeakV2.Domain.Services
{
    public class UserInstitutionService : BaseService<UserInstitution, CreateUserInstitutionViewModel, UpdateUserInstitutionViewModel, ReadUserInstitutionViewModel>,
                                          IUserInstitutionService
    {
        private readonly IUserRepository _userRepository;
        private readonly IInstitutionRepository _institutionRepository;
        private readonly IUserInstitutionRepository _userInstitutionRepository;

        public UserInstitutionService(IUserInstitutionRepository userInstitutionRepository, IUserRepository userRepository,
                                      IInstitutionRepository institutionRepository, IMapper mapper) : base(userInstitutionRepository, mapper)
        {
            _userRepository = userRepository;
            _institutionRepository = institutionRepository;
            _userInstitutionRepository = userInstitutionRepository;
        }

        public async Task<IEnumerable<ReadUserInstitutionViewModel>> UpdateAsync(IEnumerable<UpdateUserInstitutionViewModel> updateModels)
        {
            var errors = await ValidateChangeAsync(updateModels);

            if (errors.Any())
                return new[] { new ReadUserInstitutionViewModel { BaseErrors = errors } };

            var changedOnes = await GetOnlyChangedOnesAsync(updateModels);

            if (!BaseValidator.IsValid(changedOnes))
                return new[] { new ReadUserInstitutionViewModel { BaseErrors = new[] { new BaseError(BaseError.NoChangesDetected) } } };

            var models = _mapper.Map<IEnumerable<UserInstitution>>(changedOnes);
            
            models = await _userInstitutionRepository.UpdateAsync(models);                  
            return _mapper.Map<IEnumerable<ReadUserInstitutionViewModel>>(models);
        }
        
        private async Task<IEnumerable<UpdateUserInstitutionViewModel>> GetOnlyChangedOnesAsync(IEnumerable<UpdateUserInstitutionViewModel> updateModels)
        {
            if (!BaseValidator.IsValid(updateModels))
                return Enumerable.Empty<UpdateUserInstitutionViewModel>();

            var usersInstitutions = await _userInstitutionRepository.GetMembersByInstitutionFkAsync(updateModels.First().InstitutionFk);

            if (!BaseValidator.IsValid(usersInstitutions))
                return Enumerable.Empty<UpdateUserInstitutionViewModel>();

            var changedOnes = new List<UpdateUserInstitutionViewModel>();

            foreach (var updateModel in updateModels)
            {
                var userInstitution = usersInstitutions.FirstOrDefault(ui => ui.UserFk == updateModel.UserFk);

                if (!BaseValidator.IsValid(userInstitution))
                    return Enumerable.Empty<UpdateUserInstitutionViewModel>();                

                if (UserInstitutionValidator.IsEqual(userInstitution!, updateModel))
                    continue;

                changedOnes.Add(updateModel);
            }

            return changedOnes;
        }

        private async Task<IEnumerable<BaseError>> ValidateChangeAsync(IEnumerable<UpdateUserInstitutionViewModel> updateModels)
        {
            var errors = new List<BaseError>();

            if (!BaseValidator.IsValid(updateModels))
            {
                errors.Add(new BaseError(BaseError.NullObject));
                return errors;
            }

            var updateModel = updateModels.First();

            if (!updateModels.All(ui => ui.InstitutionFk == updateModel.InstitutionFk))
            {
                errors.Add(new BaseError(BaseError.InvalidValue));
                return errors;
            }

            var institution = await _institutionRepository.GetByIdAsync(updateModel.InstitutionFk);

            if (!BaseValidator.IsValid(institution))
                errors.Add(new BaseError(BaseError.InstitutionNotFound));

            return errors;
        }

        public async Task<ReadUserInstitutionViewModel> StandardRegistrationAsync(long userFk)
        {
            if (!BaseValidator.IsAbove(userFk, BaseValidator.IdMinValue))
                return new ReadUserInstitutionViewModel { BaseErrors = [new BaseError(BaseError.InvalidValue)] };

            var user = await _userRepository.GetByIdAsync(userFk);

            if (!BaseValidator.IsValid(user))
                return new ReadUserInstitutionViewModel { BaseErrors = [new BaseError(BaseError.UserNotFound)] };

            var institution = await _institutionRepository.GetStandardAsync();

            var model = new UserInstitution
            {
                UserFk = user!.Id,
                InstitutionFk = institution.Id,
                Owner = false,
                Writer = false,
                Creator = false
            };

            model = await _userInstitutionRepository.CreateAsync(model);
            return _mapper.Map<ReadUserInstitutionViewModel>(model);
        }

        protected override Func<UserInstitution, bool> ApplyFilters()
        {
            return _ => true;
        }

        protected override async Task<IEnumerable<BaseError>> ValidateCreationAsync(CreateUserInstitutionViewModel createModel)
        {
            var errors = new List<BaseError>();

            if (!BaseValidator.IsValid(createModel))
            {
                errors.Add(new BaseError(BaseError.NullObject));
                return errors;
            }

            if (!BaseValidator.IsAbove(createModel.UserFk, BaseValidator.IdMinValue))
            {
                errors.Add(new BaseError(BaseError.InvalidValue));
                return errors;
            }

            if (!BaseValidator.IsAbove(createModel.InstitutionFk, BaseValidator.IdMinValue))
            {
                errors.Add(new BaseError(BaseError.InvalidValue));
                return errors;
            }

            var user = await _userRepository.GetByIdAsync(createModel.UserFk);

            if (!BaseValidator.IsValid(user))
            {
                errors.Add(new BaseError(BaseError.UserNotFound));
                return errors;
            }

            var institution = await _institutionRepository.GetByIdAsync(createModel.InstitutionFk);

            if (!BaseValidator.IsValid(institution))
            {
                errors.Add(new BaseError(BaseError.InstitutionNotFound));
                return errors;
            }

            return errors;
        }

        public async Task<IEnumerable<ReadUserInstitutionViewModel>> GetMembersByInstitutionFkAsync(long institutionFk)
        {
            var usersIntitution = await _userInstitutionRepository.GetMembersByInstitutionFkAsync(institutionFk);

            if (!BaseValidator.IsValid(usersIntitution))
                return Enumerable.Empty<ReadUserInstitutionViewModel>();

            return _mapper.Map<IEnumerable<ReadUserInstitutionViewModel>>(usersIntitution);
        }

        public async Task<IEnumerable<ReadUserInstitutionViewModel>> GetByOwnerAsync(long userFk)
        {
            var userInstitutions = await _userInstitutionRepository.GetByOwnerAsync(userFk);

            if (!BaseValidator.IsValid(userInstitutions))
                return Enumerable.Empty<ReadUserInstitutionViewModel>();

            return _mapper.Map<IEnumerable<ReadUserInstitutionViewModel>>(userInstitutions);
        }

        public async Task<IEnumerable<ReadUserInstitutionViewModel>> GetByUserFkAsync(long userFk, bool onlyWriter = false)
        {
            var userInstitution = await _userInstitutionRepository.GetByUserFkAsync(userFk, onlyWriter);

            if (!BaseValidator.IsValid(userInstitution))
                return Enumerable.Empty<ReadUserInstitutionViewModel>();

            return _mapper.Map<IEnumerable<ReadUserInstitutionViewModel>>(userInstitution);
        }

        protected override async Task<IEnumerable<BaseError>> ValidateChangeAsync(UpdateUserInstitutionViewModel updateModel)
        {
            var errors = new List<BaseError>();

            if (!BaseValidator.IsValid(updateModel))
            {
                errors.Add(new BaseError(BaseError.NullObject));
                return errors;
            }

            if (!BaseValidator.IsAbove(updateModel.UserFk, BaseValidator.IdMinValue))
            {
                errors.Add(new BaseError(BaseError.InvalidValue));
                return errors;
            }

            if (!BaseValidator.IsAbove(updateModel.InstitutionFk, BaseValidator.IdMinValue))
            {
                errors.Add(new BaseError(BaseError.InvalidValue));
                return errors;
            }

            var userInstitution = await _userInstitutionRepository.GetByIdsAsync(new[] { updateModel.UserFk, updateModel.InstitutionFk });

            if (!BaseValidator.IsValid(userInstitution))
            {
                errors.Add(new BaseError(BaseError.UserInstitutionNotFound));
                return errors;
            }
            
            if (UserInstitutionValidator.IsEqual(userInstitution!, updateModel))
                errors.Add(new BaseError(BaseError.NoChangesDetected));

            return errors;
        }

        protected override void UpdateFields(UserInstitution model, UpdateUserInstitutionViewModel updateModel)
        {
            model.Owner = updateModel.Owner;
            model.Writer = updateModel.Writer;
        }
    }
}
