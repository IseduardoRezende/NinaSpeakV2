using AutoMapper;
using NinaSpeakV2.Data.Entities;
using NinaSpeakV2.Domain.ViewModels.ChatBotConversations;

namespace NinaSpeakV2.Domain.Profiles
{
    public class ChatBotConversationProfile : Profile
    {
        public ChatBotConversationProfile()
        {
            CreateMap<CreateChatBotConversationViewModel, ChatBotConversation>();
            CreateMap<UpdateChatBotConversationViewModel, ChatBotConversation>();
            CreateMap<ReadChatBotConversationViewModel, UpdateChatBotConversationViewModel>();
            CreateMap<ChatBotConversation, ReadChatBotConversationViewModel>();
        }
    }
}
