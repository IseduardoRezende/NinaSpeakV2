using AutoMapper;
using NinaSpeakV2.Data.Entities;
using NinaSpeakV2.Domain.ViewModels.ChatBotTypes;

namespace NinaSpeakV2.Domain.Profiles
{
    public class ChatBotTypeProfile : Profile
    {
        public ChatBotTypeProfile()
        {
            CreateMap<ChatBotType, ReadChatBotTypeViewModel>();
        }
    }
}
