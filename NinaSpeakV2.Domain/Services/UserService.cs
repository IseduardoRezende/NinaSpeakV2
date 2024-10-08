﻿using AutoMapper;
using NinaSpeakV2.Data.Entities;
using NinaSpeakV2.Data.Repositories.IRepositories;
using NinaSpeakV2.Domain.Extensions;
using NinaSpeakV2.Domain.Models;
using NinaSpeakV2.Domain.Services.IServices;
using NinaSpeakV2.Domain.Validators;
using NinaSpeakV2.Domain.ViewModels.Users;

namespace NinaSpeakV2.Domain.Services
{
    public class UserService : BaseService<User, CreateUserViewModel, UpdateUserViewModel, ReadUserViewModel>, IUserService
    {
        protected readonly IUserRepository _userRepository;
        private   readonly IInstitutionService _institutionService;
        protected readonly IUserInstitutionService _userInstitutionService;

        public UserService(IUserRepository userRepository, IUserInstitutionService userInstitutionService, IInstitutionService institutionService,
                           IMapper mapper) : base(userRepository, mapper)
        {
            _userRepository = userRepository;
            _institutionService = institutionService;
            _userInstitutionService = userInstitutionService;
        }

        public override async Task<ReadUserViewModel> CreateAsync(CreateUserViewModel createViewModel)
        {
            var errors = await ValidateCreationAsync(createViewModel);

            if (errors.Any())
                return new ReadUserViewModel { BaseErrors = errors };

            var user = await _userRepository.GetByAsync(u => u.Email == createViewModel.Email.ToLowerInvariant(), ignoreGlobalFilter: true);

            if (BaseValidator.IsValid(user))
            {
                if (!await ActiveAsync(user!, createViewModel))
                    return new ReadUserViewModel { BaseErrors = new[] { new BaseError(BaseError.InternalProcessError) } };
                
                //Send Email to Confirm

                return _mapper.Map<ReadUserViewModel>(user);
            }

            createViewModel.Salt = Guid.NewGuid().ToString();
            createViewModel.Password = createViewModel.Password.ConvertToSHA512(createViewModel.Salt);
            createViewModel.Email = createViewModel.Email.ToLowerInvariant();

            user = _mapper.Map<User>(createViewModel);
            user = await _userRepository.CreateAsync(user);
            
            var userInstitution = await _userInstitutionService.StandardRegistrationAsync(user.Id);

            if (userInstitution.HasErrors())
                return new ReadUserViewModel { BaseErrors = userInstitution.BaseErrors };

            //Send Email to Confirm

            return _mapper.Map<ReadUserViewModel>(user);
        }

        public override async Task<ReadUserViewModel> UpdateAsync(UpdateUserViewModel updateViewModel)
        {
            var errors = await ValidateChangeAsync(updateViewModel);

            if (errors.Any())
                return new ReadUserViewModel { BaseErrors = errors };

            var entity = await _userRepository.GetByIdAsync(updateViewModel.Id);

            if (!BaseValidator.IsValid(entity))
                return new ReadUserViewModel { BaseErrors = new[] { new BaseError(BaseError.InexistentObject) } };

            UpdateFields(entity!, updateViewModel);
            entity = await _userRepository.UpdateAsync(entity!);
            
            //Send Email to Confirm
            
            return _mapper.Map<ReadUserViewModel>(entity);
        }

        public async Task<ReadUserViewModel> UpdatePasswordAsync(UpdateUserPasswordViewModel updateViewModel)
        {
            var errors = await ValidadeUpdatePasswordAsync(updateViewModel);

            if (errors.Any())
                return new ReadUserViewModel { BaseErrors = errors };

            var entity = await _userRepository.GetByIdAsync(updateViewModel.Id);

            if (!BaseValidator.IsValid(entity))
                return new ReadUserViewModel { BaseErrors = new[] { new BaseError(BaseError.InexistentObject) } };

            UpdatePassword(entity!, updateViewModel);

            entity = await _userRepository.UpdateAsync(entity!);
            return _mapper.Map<ReadUserViewModel>(entity);
        }

        public override async Task<bool> SoftDeleteAsync(long id)
        {
            if (!await base.SoftDeleteAsync(id))
                return false;

            if (!await _userInstitutionService.SoftDeleteByUserFkAsync(id))
            {
                await base.ActiveAsync(id);
                return false;
            }

            return true;
        }

        private async Task<bool> ActiveAsync(User user, CreateUserViewModel createUser)
        {
            if (!BaseValidator.IsValid(user) || user!.DeletedAt is null || !BaseValidator.IsValid(createUser))
                return false;

            user.DeletedAt = null;
            user.Salt = Guid.NewGuid().ToString();
            user.Password = createUser.Password.ConvertToSHA512(user.Salt);
            user.VerificationCode = createUser.VerificationCode;

            user = await _userRepository.UpdateAsync(user!);

            if (!BaseValidator.IsValid(user))
                return false;

            var standardInstitution = await _institutionService.GetStandardAsync();

            if (!await _userInstitutionService.ActiveAsync(userFk: user.Id, institutionFk: (long)standardInstitution.Id!))
            {
                await _userRepository.SoftDeleteAsync(user);
                return false;
            }

            return true;
        }
       
        protected override Func<User, bool> ApplyFilters()
        {
            return _ => true;
        }

        protected override async Task<IEnumerable<BaseError>> ValidateCreationAsync(CreateUserViewModel createViewModel)
        {
            var errors = new List<BaseError>();            

            if (!BaseValidator.IsValid(createViewModel))
            {
                errors.Add(new BaseError(BaseError.NullObject));
                return errors;
            }

            if (createViewModel.Password != createViewModel.ConfirmPassword)
                errors.Add(new BaseError(BaseError.PasswordNotMatch));

            if (!UserValidator.IsValidPassword(createViewModel.Password))
                errors.Add(new BaseError(BaseError.InvalidPassword));

            if (!UserValidator.IsValidEmail(createViewModel.Email))
                errors.Add(new BaseError(BaseError.InvalidEmail));

            if (await _userRepository.EmailAlreadyExistAsync(createViewModel.Email))
                errors.Add(new BaseError(BaseError.EmailAlreadyExist));

            return errors;
        }

        protected override async Task<IEnumerable<BaseError>> ValidateChangeAsync(UpdateUserViewModel updateViewModel)
        {
            var errors = new List<BaseError>();

            if (!BaseValidator.IsValid(updateViewModel))
            {
                errors.Add(new BaseError(BaseError.NullObject));
                return errors;
            }
            
            if (!updateViewModel.NewEmail.Equals(updateViewModel.ConfirmNewEmail))
                errors.Add(new BaseError(BaseError.EmailNotMatch));

            if (!UserValidator.IsValidEmail(updateViewModel.Email) || !UserValidator.IsValidEmail(updateViewModel.NewEmail))
                errors.Add(new BaseError(BaseError.InvalidEmail));

            var user = await _userRepository.GetByIdAsync(updateViewModel.Id);

            if (!BaseValidator.IsValid(user))
            {
                errors.Add(new BaseError(BaseError.UserNotFound));
                return errors;
            }

            if (!user!.Email.Equals(updateViewModel.Email, StringComparison.InvariantCultureIgnoreCase))
                errors.Add(new BaseError(BaseError.InvalidEmail));

            if (user.Email.Equals(updateViewModel.NewEmail, StringComparison.InvariantCultureIgnoreCase)) //Create an abstract implementation (IsEqual) foreach [Model]Validator
                errors.Add(new BaseError(BaseError.NoChangesDetected));
            
            if (!await _userRepository.CanChangeEmailAsync(user, updateViewModel.NewEmail))
                errors.Add(new BaseError(BaseError.EmailAlreadyExist));

            return errors;
        }

        private async Task<IEnumerable<BaseError>> ValidadeUpdatePasswordAsync(UpdateUserPasswordViewModel updateViewModel)
        {
            var errors = new List<BaseError>();

            if (!BaseValidator.IsValid(updateViewModel))
            {
                errors.Add(new BaseError(BaseError.NullObject));
                return errors;
            }
            
            if (!updateViewModel.NewPassword.Equals(updateViewModel.ConfirmNewPassword))
                errors.Add(new BaseError(BaseError.PasswordNotMatch));

            if (!UserValidator.IsValidPassword(updateViewModel.Password) || !UserValidator.IsValidPassword(updateViewModel.NewPassword))
                errors.Add(new BaseError(BaseError.InvalidPassword));

            var user = await _userRepository.GetByIdAsync(updateViewModel.Id);

            if (!BaseValidator.IsValid(user))
            {
                errors.Add(new BaseError(BaseError.UserNotFound));
                return errors;
            }

            var currentPassword = updateViewModel.Password.ConvertToSHA512(user!.Salt);

            if (!currentPassword.Equals(user.Password))
                errors.Add(new BaseError(BaseError.InvalidPassword));

            var newPassword = updateViewModel.NewPassword.ConvertToSHA512(user!.Salt);

            if (newPassword.Equals(user.Password))
                errors.Add(new BaseError(BaseError.NoChangesDetected));
                        
            return errors;
        }

        protected override void UpdateFields(User entity, UpdateUserViewModel updateViewModel)
        {
            entity.Email = updateViewModel.NewEmail.ToLowerInvariant();
        }

        private void UpdatePassword(User entity, UpdateUserPasswordViewModel updateViewModel) 
        {
            entity.Salt = Guid.NewGuid().ToString();
            entity.Password = updateViewModel.NewPassword.ConvertToSHA512(entity.Salt);
        }
    }
}
