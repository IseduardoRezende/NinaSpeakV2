using AutoMapper;
using NinaSpeakV2.Data.Repositories.IRepositories;
using NinaSpeakV2.Domain.Entities;
using NinaSpeakV2.Domain.Extensions;
using NinaSpeakV2.Domain.Services.IServices;
using NinaSpeakV2.Domain.Validators;
using NinaSpeakV2.Domain.ViewModels.Login;
using NinaSpeakV2.Domain.ViewModels.Users;
using NinaSpeakV2.Domain.ViewModels.UsersInstitutions;

namespace NinaSpeakV2.Domain.Services
{
    public class LoginService : UserService, ILoginService
    {
        public LoginService(IUserRepository userRepository, IUserInstitutionService userInstitutionService, IMapper mapper) 
            : base(userRepository, userInstitutionService, mapper)
        { }

        public async Task<ReadUserViewModel> LoginAsync(ReadLoginViewModel login)
        {
            var errors = await ValidateLoginAsync(login);

            if (errors.Any())            
                return new ReadUserViewModel { BaseErrors = errors };            
                    
            var user = await base.GetByAsync(u => u.Email == login.Email.ToLowerInvariant());

            if (!BaseValidator.IsValid(user))
                return new ReadUserViewModel { BaseErrors = new[] { new BaseError(BaseError.UserNotFound) } };

            return user!;
        }

        public Task<ReadUserViewModel> RegisterAsync(CreateLoginViewModel login)
        {
            return base.CreateAsync(login);
        }

        public async Task<ReadUserViewModel> RegisterAsync(CreateUserInstitutionViewModel userInstitution)
        {
            if (!BaseValidator.IsValid(userInstitution))
                return new ReadUserViewModel { BaseErrors = new[] { new BaseError(BaseError.NullObject) } };

            var login = new CreateUserViewModel 
            { 
                Email = userInstitution.UserEmail,
                Password = userInstitution.UserPassword,
                ConfirmPassword = userInstitution.UserConfirmPassword
            };

            var result = await base.CreateAsync(login);

            if (result.HasErrors())
                return result;

            var value = await _userInstitutionService.CreateAsync(userInstitution);

            if (value.HasErrors())
                return new ReadUserViewModel { BaseErrors = value.BaseErrors };
            
            return result;                
        }

        private async Task<IEnumerable<BaseError>> ValidateLoginAsync(ReadLoginViewModel login)
        {
            var errors = new List<BaseError>();

            if (!BaseValidator.IsValid(login))
            {
                errors.Add(new BaseError(BaseError.NullObject));
                return errors;
            }

            if (!UserValidator.IsValidPassword(login.Password))
                errors.Add(new BaseError(BaseError.InvalidPassword));

            if (!UserValidator.IsValidEmail(login.Email))
                errors.Add(new BaseError(BaseError.InvalidEmail));

            var user = await _userRepository.GetByAsync(u => u.Email == login.Email.ToLowerInvariant());

            if (!BaseValidator.IsValid(user))
            {
                errors.Add(new BaseError(BaseError.UserNotFound));
                return errors;
            }
            
            var password = login.Password.ConvertToSHA512(user!.Salt);

            if (password != user.Password)
                errors.Add(new BaseError(BaseError.InvalidPassword));

            return errors;
        }
    }
}
