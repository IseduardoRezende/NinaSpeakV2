using AutoMapper;
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
        protected readonly IUserInstitutionService _userInstitutionService;

        public UserService(IUserRepository userRepository, IUserInstitutionService userInstitutionService,
            IMapper mapper) : base(userRepository, mapper)
        {
            _userRepository = userRepository;
            _userInstitutionService = userInstitutionService;
        }

        //TODO: DELETE ACTION

        public override async Task<ReadUserViewModel> CreateAsync(CreateUserViewModel createViewModel)
        {
            var errors = await ValidateCreationAsync(createViewModel);

            if (errors.Any())
                return new ReadUserViewModel { BaseErrors = errors };

            createViewModel.Salt = Guid.NewGuid().ToString();
            createViewModel.Password = createViewModel.Password.ConvertToSHA512(createViewModel.Salt);
            createViewModel.Email = createViewModel.Email.ToLowerInvariant();

            var entity = _mapper.Map<User>(createViewModel);
            entity = await _userRepository.CreateAsync(entity);
            
            var userInstitution = await _userInstitutionService.StandardRegistrationAsync(entity.Id);

            if (userInstitution.HasErrors())
                return new ReadUserViewModel { BaseErrors = userInstitution.BaseErrors };

            //Send Email to Confirm

            return _mapper.Map<ReadUserViewModel>(entity);
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
            
            if (updateViewModel.Password != updateViewModel.ConfirmPassword)
                errors.Add(new BaseError(BaseError.PasswordNotMatch));

            if (!UserValidator.IsValidPassword(updateViewModel.Password))
                errors.Add(new BaseError(BaseError.InvalidPassword));

            if (!UserValidator.IsValidEmail(updateViewModel.Email))
                errors.Add(new BaseError(BaseError.InvalidEmail));

            var user = await _userRepository.GetByIdAsync(updateViewModel.Id);

            if (!BaseValidator.IsValid(user))
            {
                errors.Add(new BaseError(BaseError.UserNotFound));
                return errors;
            }

            if (user!.Email.Equals(updateViewModel.Email, StringComparison.InvariantCultureIgnoreCase)) //Create an abstract implementation (IsEqual) foreach [Model]Validator
                errors.Add(new BaseError(BaseError.NoChangesDetected));

            var password = updateViewModel.Password.ConvertToSHA512(user.Salt);

            if (user.Password != password)
                errors.Add(new BaseError(BaseError.InvalidPassword));

            if (!await _userRepository.CanChangeEmailAsync(user, updateViewModel.Email))
                errors.Add(new BaseError(BaseError.EmailAlreadyExist));

            return errors;
        }

        protected override void UpdateFields(User entity, UpdateUserViewModel updateViewModel)
        {
            entity.Email = updateViewModel.Email.ToLowerInvariant();

            //Authenticated = false
        }
    }
}
