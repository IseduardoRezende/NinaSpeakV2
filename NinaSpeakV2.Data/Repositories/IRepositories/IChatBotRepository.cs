using NinaSpeakV2.Data.Models;

namespace NinaSpeakV2.Data.Repositories.IRepositories
{
    public interface IChatBotRepository : IBaseRepository<ChatBot>
    {
        Task<IEnumerable<ChatBot>> GetByInstitutionsFks(IEnumerable<long> institutionsFks);

        Task<bool> NameAlreadyExistAsync(long institutionFk, string name);
        
        Task<bool> CanChangeNameAsync(ChatBot chatBot, long institutionFk, string newName);
    }
}
