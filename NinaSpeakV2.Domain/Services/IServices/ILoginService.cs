using NinaSpeakV2.Domain.ViewModels.Login;
using NinaSpeakV2.Domain.ViewModels.Users;
using NinaSpeakV2.Domain.ViewModels.UsersInstitutions;

namespace NinaSpeakV2.Domain.Services.IServices
{
    public interface ILoginService
    {
        Task<ReadUserViewModel> LoginAsync(ReadLoginViewModel login);

        Task<ReadUserViewModel> RegisterAsync(CreateLoginViewModel login);

        Task<ReadUserViewModel> RegisterAsync(CreateUserInstitutionViewModel userInstitution);
    }
}
