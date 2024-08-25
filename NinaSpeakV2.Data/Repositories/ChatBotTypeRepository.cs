using NinaSpeakV2.Data.Entities;
using NinaSpeakV2.Data.Repositories.IRepositories;

namespace NinaSpeakV2.Data.Repositories
{
    public class ChatBotTypeRepository : BaseReadonlyRepository<ChatBotType>, IChatBotTypeRepository
    {
        public ChatBotTypeRepository(NinaSpeakContext context) : base(context) { }
    }
}
