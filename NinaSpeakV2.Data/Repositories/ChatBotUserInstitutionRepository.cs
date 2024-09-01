using Microsoft.EntityFrameworkCore;
using NinaSpeakV2.Data.Entities;
using NinaSpeakV2.Data.Repositories.IRepositories;

namespace NinaSpeakV2.Data.Repositories
{
    public class ChatBotUserInstitutionRepository : BaseRepository<ChatBotUserInstitution>, IChatBotUserInstitutionRepository
    {
        public ChatBotUserInstitutionRepository(NinaSpeakContext context) : base(context) {  }

        public async Task<IEnumerable<ChatBotUserInstitution>> GetMembersByChatBotFkAsync(long chatBotFk)
        {
            if (chatBotFk <= default(long))
                return Enumerable.Empty<ChatBotUserInstitution>();

            return await Entity
                        .Include(cbui => cbui.ChatBot)
                        .Include(cbui => cbui.UserInstitution)
                            .ThenInclude(ui => ui.User)
                        .Where(cbui => cbui.ChatBot.Id == chatBotFk)
                        .OrderBy(cbui => cbui.UserInstitution.User.Email)
                        .ToListAsync();
        }

        public async Task<IEnumerable<ChatBotUserInstitution>> GetByUserIdAsync(long userId)
        {
            if (userId <= default(long))
                return Enumerable.Empty<ChatBotUserInstitution>();

            return await Entity
                        .Include(cbui => cbui.ChatBot)
                        .Include(cbui => cbui.UserInstitution)
                            .ThenInclude(ui => ui.Institution)
                        .Where(cbui => cbui.UserInstitution.UserFk == userId)
                        .ToListAsync();
        }
    }
}
