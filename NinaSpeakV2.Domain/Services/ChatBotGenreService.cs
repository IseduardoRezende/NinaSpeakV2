using AutoMapper;
using NinaSpeakV2.Data.Entities;
using NinaSpeakV2.Data.Repositories.IRepositories;
using NinaSpeakV2.Domain.Services.IServices;
using NinaSpeakV2.Domain.ViewModels.ChatBotGenres;

namespace NinaSpeakV2.Domain.Services
{
    public class ChatBotGenreService : BaseReadonlyService<ChatBotGenre, ReadChatBotGenreViewModel>, IChatBotGenreService
    {
        public ChatBotGenreService(IChatBotGenreRepository chatBotGenreRepository, IMapper mapper) : base(chatBotGenreRepository, mapper)
        { }

        protected override Func<ChatBotGenre, bool> ApplyFilters()
        {
            return _ => true;
        }
    }
}
