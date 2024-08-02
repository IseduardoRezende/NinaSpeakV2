using Microsoft.EntityFrameworkCore;
using NinaSpeakV2.Data.Entities;
using NinaSpeakV2.Data.Repositories.IRepositories;

namespace NinaSpeakV2.Data.Repositories
{
    public class UserRepository : BaseRepository<User>, IUserRepository
    {
        public UserRepository(NinaSpeakContext context) : base(context) { }        

        public async Task<bool> EmailAlreadyExistAsync(string email)
        {
            if (string.IsNullOrEmpty(email) || string.IsNullOrWhiteSpace(email))
                throw new ArgumentException("Invalid Email");

            return await Entity.AnyAsync(u => u.Email == email.ToLowerInvariant());
        }

        public async Task<bool> CanChangeEmailAsync(User user, string newEmail)
        {
            ArgumentNullException.ThrowIfNull(user, nameof(user));

            if (string.IsNullOrEmpty(newEmail) || string.IsNullOrWhiteSpace(newEmail))
                throw new ArgumentException("Invalid Email");

            var emailOwner = await GetByAsync(u => u.Email == newEmail.ToLowerInvariant());
            return emailOwner is null || emailOwner.Id == user.Id;
        }
    }
}
