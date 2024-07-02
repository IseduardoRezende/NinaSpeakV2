using NinaSpeakV2.Data.Models;

namespace NinaSpeakV2.Data.Repositories.IRepositories
{
    public interface IUserRepository : IBaseRepository<User>
    {
        Task<bool> EmailAlreadyExistAsync(string email);

        Task<bool> CanChangeEmailAsync(User user, string newEmail);
    }
}
