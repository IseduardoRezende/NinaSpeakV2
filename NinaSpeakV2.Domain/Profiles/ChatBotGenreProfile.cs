using AutoMapper;
using NinaSpeakV2.Data.Models;
using NinaSpeakV2.Domain.ViewModels.ChatBotGenres;

namespace NinaSpeakV2.Domain.Profiles
{
    public class ChatBotGenreProfile : Profile
    {
        public ChatBotGenreProfile()
        {
            CreateMap<ChatBotGenre, ReadChatBotGenreViewModel>();
        }
    }
}
