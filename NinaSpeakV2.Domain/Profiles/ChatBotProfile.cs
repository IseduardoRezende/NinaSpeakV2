using AutoMapper;
using NinaSpeakV2.Data.Models;
using NinaSpeakV2.Domain.ViewModels.ChatBots;

namespace NinaSpeakV2.Domain.Profiles
{
    public class ChatBotProfile : Profile
    {
        public ChatBotProfile()
        {
            CreateMap<CreateChatBotViewModel, ChatBot>();
            CreateMap<UpdateChatBotViewModel, ChatBot>();
            CreateMap<ReadChatBotViewModel, UpdateChatBotViewModel>();
            CreateMap<ChatBot, ReadChatBotViewModel>();
        }
    }
}
