using NinaSpeakV2.Data.Models;
using NinaSpeakV2.Data.Repositories.IRepositories;

namespace NinaSpeakV2.Data.Repositories
{
    public class ChatBotConversationRepository : BaseRepository<ChatBotConversation>, IChatBotConversationRepository
    {
        public ChatBotConversationRepository(NinaSpeakContext context) : base(context) { }
    }
}
