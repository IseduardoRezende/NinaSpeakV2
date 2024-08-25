using NinaSpeakV2.Data.Entities;
using NinaSpeakV2.Data.Repositories.IRepositories;

namespace NinaSpeakV2.Data.Repositories
{
    public class ChatBotUserInstitutionRepository : BaseRepository<ChatBotUserInstitution>, IChatBotUserInstitutionRepository
    {
        public ChatBotUserInstitutionRepository(NinaSpeakContext context) : base(context) {  }
    }
}
