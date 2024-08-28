using AutoMapper;
using NinaSpeakV2.Data.Entities;
using NinaSpeakV2.Data.Repositories.IRepositories;
using NinaSpeakV2.Domain.Extensions;
using NinaSpeakV2.Domain.Models;
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
        
        public override async Task<ReadUserInstitutionViewModel> CreateAsync(CreateUserInstitutionViewModel createViewModel)
        {
            var errors = await ValidateCreationAsync(createViewModel);

            if (errors.Any())
                return new ReadUserInstitutionViewModel { BaseErrors = errors };

            var user = await _userRepository.GetByAsync(u => u.Email == createViewModel.UserEmail.ToLowerInvariant());                       
            createViewModel.UserFk = user!.Id;

            var userInstitution = await _userInstitutionRepository
                                        .GetByAsync(c => c.UserFk == createViewModel.UserFk && c.InstitutionFk == createViewModel.InstitutionFk, ignoreGlobalFilter: true);

            if (BaseValidator.IsValid(userInstitution))
            {
                await _userInstitutionRepository.ActiveAsync(userInstitution!);
            }
            else
            {
                userInstitution = _mapper.Map<UserInstitution>(createViewModel);            
                userInstitution = await _userInstitutionRepository.CreateAsync(userInstitution);       
            }

            await _institutionRepository.UpdateCodeAsync(createViewModel.InstitutionFk);            
            var readViewModel = _mapper.Map<ReadUserInstitutionViewModel>(userInstitution);            
        
            readViewModel.UserEmail = user.Email;
            return readViewModel;
        }

        public async Task<IEnumerable<ReadUserInstitutionViewModel>> UpdateAsync(IEnumerable<UpdateUserInstitutionViewModel> updateViewModels)
        {
            var errors = await ValidateChangeAsync(updateViewModels);

            if (errors.Any())
                return new[] { new ReadUserInstitutionViewModel { BaseErrors = errors } };

            var changedOnes = await GetOnlyChangedOnesAsync(updateViewModels);

            if (!BaseValidator.IsValid(changedOnes))
                return new[] { new ReadUserInstitutionViewModel { BaseErrors = new[] { new BaseError(BaseError.NoChangesDetected) } } };

            var entities = _mapper.Map<IEnumerable<UserInstitution>>(changedOnes);
            entities = await _userInstitutionRepository.UpdateAsync(entities);
            return _mapper.Map<IEnumerable<ReadUserInstitutionViewModel>>(entities);
        }

        private async Task<IEnumerable<UpdateUserInstitutionViewModel>> GetOnlyChangedOnesAsync(IEnumerable<UpdateUserInstitutionViewModel> updateViewModels)
        {
            if (!BaseValidator.IsValid(updateViewModels))
                return Enumerable.Empty<UpdateUserInstitutionViewModel>();

            var usersInstitutions = await _userInstitutionRepository.GetMembersByInstitutionFkAsync(updateViewModels.First().InstitutionFk);

            if (!BaseValidator.IsValid(usersInstitutions))
                return Enumerable.Empty<UpdateUserInstitutionViewModel>();

            var changedOnes = new List<UpdateUserInstitutionViewModel>();

            foreach (var updateModel in updateViewModels)
            {
                var userInstitution = usersInstitutions.FirstOrDefault(ui => ui.UserFk == updateModel.UserFk);

                if (!BaseValidator.IsValid(userInstitution))
                    return Enumerable.Empty<UpdateUserInstitutionViewModel>();

                if (userInstitution!.Creator || UserInstitutionValidator.IsEqual(userInstitution, updateModel))
                    continue;

                changedOnes.Add(updateModel);
            }

            return changedOnes;
        }

        private async Task<IEnumerable<BaseError>> ValidateChangeAsync(IEnumerable<UpdateUserInstitutionViewModel> updateViewModels)
        {
            var errors = new List<BaseError>();

            if (!BaseValidator.IsValid(updateViewModels))
            {
                errors.Add(new BaseError(BaseError.NullObject));
                return errors;
            }

            var updateModel = updateViewModels.First();

            if (!updateViewModels.All(ui => ui.InstitutionFk == updateModel.InstitutionFk))
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

            var userInstitution = new UserInstitution
            {
                UserFk = user!.Id,
                InstitutionFk = institution.Id,
                Owner = false,
                Creator = false
            };

            userInstitution = await _userInstitutionRepository.CreateAsync(userInstitution);
            return _mapper.Map<ReadUserInstitutionViewModel>(userInstitution);
        }       

        protected override Func<UserInstitution, bool> ApplyFilters()
        {
            return _ => true;
        }
        
        protected override async Task<IEnumerable<BaseError>> ValidateCreationAsync(CreateUserInstitutionViewModel createViewModel)
        {
            var errors = new List<BaseError>();

            if (!BaseValidator.IsValid(createViewModel))
            {
                errors.Add(new BaseError(BaseError.NullObject));
                return errors;
            }
            
            if (!BaseValidator.IsAbove(createViewModel.InstitutionFk, BaseValidator.IdMinValue))
                errors.Add(new BaseError(BaseError.InvalidValue));

            if (!UserValidator.IsValidPassword(createViewModel.UserPassword))
                errors.Add(new BaseError(BaseError.InvalidPassword));

            if (!UserValidator.IsValidEmail(createViewModel.UserEmail))
                errors.Add(new BaseError(BaseError.InvalidEmail));

            if (!InstitutionValidator.IsValidCode(createViewModel.InstitutionCode))
                errors.Add(new BaseError(BaseError.InvalidCode));

            var institution = await _institutionRepository.GetByIdAsync(createViewModel.InstitutionFk);

            if (!BaseValidator.IsValid(institution))
            {
                errors.Add(new BaseError(BaseError.InstitutionNotFound));
                return errors;
            }

            var user = await _userRepository.GetByAsync(u => u.Email == createViewModel.UserEmail.ToLowerInvariant());
            
            if (!BaseValidator.IsValid(user))
            {
                errors.Add(new BaseError(BaseError.UserNotFound));
                return errors;
            }
                        
            if (!UserValidator.IsAuthenticated(user!))
                errors.Add(new BaseError(BaseError.UserNotAuthenticated));

            var password = createViewModel.UserPassword.ConvertToSHA512(user!.Salt);

            if (password != user.Password)
                errors.Add(new BaseError(BaseError.InvalidPassword));

            var members = await GetMembersByInstitutionFkAsync(createViewModel.InstitutionFk);             

            if (members.Any(m => m.UserFk == user.Id))
                errors.Add(new BaseError(BaseError.UserInstitutionAlreadyExist));

            return errors;
        }

        public async Task<bool> SoftDeleteByUserFkAsync(long userFk)
        {
            if (!BaseValidator.IsAbove(userFk, BaseValidator.IdMinValue))
                return false;

            var userInstitutions = await GetByUserFkAsync(userFk, ignoreGlobalFilter: true);

            if (!BaseValidator.IsValid(userInstitutions))
                return false;

            foreach (var userInstitution in userInstitutions)
            {
                if (!await SoftDeleteAsync(userFk, userInstitution.InstitutionFk))
                    return false;  //RollBack (Active) | Add Log ?
            }

            return true;
        }

        public async Task<bool> SoftDeleteByInstitutionFkAsync(long institutionFk)
        {
            if (!BaseValidator.IsAbove(institutionFk, BaseValidator.IdMinValue))
                return false;

            var readViewModels = await GetMembersByInstitutionFkAsync(institutionFk, ignoreGlobalFilter: true);

            if (!BaseValidator.IsValid(readViewModels))
                return false;

            var userInstitutions = _mapper.Map<IEnumerable<UserInstitution>>(readViewModels.Where(ui => ui.DeletedAt is null));
            return await _userInstitutionRepository.SoftDeleteAsync(userInstitutions);
        }

        public async Task<bool> SoftDeleteAsync(long userFk, long institutionFk)
        {
            var entity = await _userInstitutionRepository.GetByAsync(c => c.UserFk == userFk && c.InstitutionFk == institutionFk);

            if (!BaseValidator.IsValid(entity))
                return false;

            if (!await _userInstitutionRepository.SoftDeleteAsync(entity!))
                return false;

            var members = await GetMembersByInstitutionFkAsync(institutionFk);

            if (!members.Any())
            {
                var institution = await _institutionRepository.GetByIdAsync(institutionFk);
                return await _institutionRepository.SoftDeleteAsync(institution!);
            }

            if (!members.Any(m => m.Creator || m.Owner))
                await ChooseNewOwner(members);

            return true;
        }

        public async Task<bool> ActiveAsync(long userFk, long institutionFk)
        {
            var entity = await _userInstitutionRepository.GetByAsync(c => c.UserFk == userFk && c.InstitutionFk == institutionFk, ignoreGlobalFilter: true);

            if (!BaseValidator.IsValid(entity))
                return false;
            
            return await _userInstitutionRepository.ActiveAsync(entity!);
        }

        private async Task ChooseNewOwner(IEnumerable<ReadUserInstitutionViewModel> members)
        {
            if (!BaseValidator.IsValid(members))
                return;

            var oldestMember = members.MinBy(m => m.CreatedAt);
            oldestMember!.Owner = true;

            var model = _mapper.Map<UserInstitution>(oldestMember);
            await _userInstitutionRepository.UpdateAsync(model);
        }

        public async Task<IEnumerable<ReadUserInstitutionViewModel>> GetMembersByInstitutionFkAsync(long institutionFk, bool ignoreGlobalFilter = false)
        {
            if (!BaseValidator.IsAbove(institutionFk, BaseValidator.IdMinValue))
                return Enumerable.Empty<ReadUserInstitutionViewModel>();
            
            var usersIntitution = await _userInstitutionRepository.GetMembersByInstitutionFkAsync(institutionFk, ignoreGlobalFilter);

            if (!BaseValidator.IsValid(usersIntitution))
                return Enumerable.Empty<ReadUserInstitutionViewModel>();

            return _mapper.Map<IEnumerable<ReadUserInstitutionViewModel>>(usersIntitution);
        }

        public async Task<IEnumerable<ReadUserInstitutionViewModel>> GetByOwnerAsync(long userFk, bool ignoreGlobalFilter = false)
        {
            if (!BaseValidator.IsAbove(userFk, BaseValidator.IdMinValue))
                return Enumerable.Empty<ReadUserInstitutionViewModel>();

            var userInstitutions = await _userInstitutionRepository.GetByOwnerAsync(userFk, ignoreGlobalFilter);

            if (!BaseValidator.IsValid(userInstitutions))
                return Enumerable.Empty<ReadUserInstitutionViewModel>();

            userInstitutions = userInstitutions.OrderBy(c => c.Institution.Name);

            return _mapper.Map<IEnumerable<ReadUserInstitutionViewModel>>(userInstitutions);
        }

        public async Task<IEnumerable<ReadUserInstitutionViewModel>> GetByUserFkAsync(long userFk, bool ignoreGlobalFilter = false)
        {
            if (!BaseValidator.IsAbove(userFk, BaseValidator.IdMinValue))
                return Enumerable.Empty<ReadUserInstitutionViewModel>();

            var userInstitution = await _userInstitutionRepository.GetByUserFkAsync(userFk, ignoreGlobalFilter);

            if (!BaseValidator.IsValid(userInstitution))
                return Enumerable.Empty<ReadUserInstitutionViewModel>();

            return _mapper.Map<IEnumerable<ReadUserInstitutionViewModel>>(userInstitution);
        }

        protected override async Task<IEnumerable<BaseError>> ValidateChangeAsync(UpdateUserInstitutionViewModel updateViewModel)
        {
            var errors = new List<BaseError>();

            if (!BaseValidator.IsValid(updateViewModel))
            {
                errors.Add(new BaseError(BaseError.NullObject));
                return errors;
            }

            if (!BaseValidator.IsAbove(updateViewModel.UserFk, BaseValidator.IdMinValue))
            {
                errors.Add(new BaseError(BaseError.InvalidValue));
                return errors;
            }

            if (!BaseValidator.IsAbove(updateViewModel.InstitutionFk, BaseValidator.IdMinValue))
            {
                errors.Add(new BaseError(BaseError.InvalidValue));
                return errors;
            }

            var userInstitution = await _userInstitutionRepository.GetByAsync(c => c.UserFk == updateViewModel.UserFk && c.InstitutionFk == updateViewModel.InstitutionFk);

            if (!BaseValidator.IsValid(userInstitution))
            {
                errors.Add(new BaseError(BaseError.UserInstitutionNotFound));
                return errors;
            }

            if (UserInstitutionValidator.IsEqual(userInstitution!, updateViewModel))
                errors.Add(new BaseError(BaseError.NoChangesDetected));

            return errors;
        }

        protected override void UpdateFields(UserInstitution entity, UpdateUserInstitutionViewModel updateViewModel)
        {
            entity.Owner = updateViewModel.Owner;
        }
    }
}
