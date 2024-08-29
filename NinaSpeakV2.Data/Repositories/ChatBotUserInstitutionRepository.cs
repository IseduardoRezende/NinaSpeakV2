using Microsoft.EntityFrameworkCore;
using NinaSpeakV2.Data.Entities;
using NinaSpeakV2.Data.Repositories.IRepositories;

namespace NinaSpeakV2.Data.Repositories
{
    public class ChatBotUserInstitutionRepository : BaseRepository<ChatBotUserInstitution>, IChatBotUserInstitutionRepository
    {
        public ChatBotUserInstitutionRepository(NinaSpeakContext context) : base(context) {  }

        public async Task<IEnumerable<ChatBotUserInstitution>> GetByUserIdAsync(long userId)
        {
            return await Task.FromResult(Entity
                                        .Include(cbui => cbui.UserInstitution)
                                        .ThenInclude(ui => ui.Institution)
                                        .Include(cbui => cbui.ChatBot)
                                        .Where(cbui => cbui.UserInstitution.UserFk == userId));
        }
    }
}
