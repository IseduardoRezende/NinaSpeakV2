using NinaSpeakV2.Data.Entities;

namespace NinaSpeakV2.Data.Repositories.IRepositories
{
    public interface IChatBotUserInstitutionRepository : IBaseRepository<ChatBotUserInstitution>
    {
        Task<IEnumerable<ChatBotUserInstitution>> GetByUserIdAsync(long userId);
    }
}
