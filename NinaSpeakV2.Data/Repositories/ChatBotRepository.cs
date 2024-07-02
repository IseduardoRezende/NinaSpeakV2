using Microsoft.EntityFrameworkCore;
using NinaSpeakV2.Data.Models;
using NinaSpeakV2.Data.Repositories.IRepositories;

namespace NinaSpeakV2.Data.Repositories
{
    public class ChatBotRepository : BaseRepository<ChatBot>, IChatBotRepository
    {
        public ChatBotRepository(NinaSpeakContext context) : base(context) { }

        public async Task<IEnumerable<ChatBot>> GetByInstitutionsFks(IEnumerable<long> institutionsFks)
        {
            if (institutionsFks is null || institutionsFks.Any(v => v <= default(long)))
                return Enumerable.Empty<ChatBot>();

            return await Task.FromResult(Model.Where(c => institutionsFks.Contains(c.InstitutionFk)));
        }

        public async Task<bool> NameAlreadyExistAsync(long institutionFk, string name)
        {
            if (string.IsNullOrEmpty(name) || string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Invalid Name");

            if (institutionFk <= default(long))
                throw new ArgumentException("Invalid institutionFk");

            return await Model.AnyAsync(c => c.InstitutionFk == institutionFk && c.Name == name);
        }

        public async Task<bool> CanChangeNameAsync(ChatBot chatBot, long institutionFk, string newName)
        {
            ArgumentNullException.ThrowIfNull(chatBot, nameof(chatBot));

            if (string.IsNullOrEmpty(newName) || string.IsNullOrWhiteSpace(newName))
                throw new ArgumentException("Invalid Name");

            if (institutionFk <= default(long))
                throw new ArgumentException("Invalid institutionFk");

            var nameOwner = await base.GetByAsync(c => c.InstitutionFk == institutionFk && c.Name == newName);
            return nameOwner is null || nameOwner.Id == chatBot.Id;
        }
    }
}
