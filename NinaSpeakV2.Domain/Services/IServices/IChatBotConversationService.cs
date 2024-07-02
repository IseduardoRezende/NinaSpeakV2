using NinaSpeakV2.Data.Models;
using NinaSpeakV2.Domain.ViewModels.ChatBotConversations;

namespace NinaSpeakV2.Domain.Services.IServices
{
    public interface IChatBotConversationService 
        : IBaseService<ChatBotConversation, CreateChatBotConversationViewModel, UpdateChatBotConversationViewModel, ReadChatBotConversationViewModel>
    {
    }
}
