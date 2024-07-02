using NinaSpeakV2.Domain.ViewModels.Login;
using NinaSpeakV2.Domain.ViewModels.Users;

namespace NinaSpeakV2.Domain.Services.IServices
{
    public interface ILoginService
    {
        Task<ReadUserViewModel> LoginAsync(ReadLoginViewModel login);

        Task<ReadUserViewModel> RegisterAsync(CreateLoginViewModel login);
    }
}
