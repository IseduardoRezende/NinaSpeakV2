using NinaSpeakV2.Data.Models;
using NinaSpeakV2.Domain.ViewModels.ChatBotGenres;

namespace NinaSpeakV2.Domain.Services.IServices
{
    public interface IChatBotGenreService : IBaseReadonlyService<ChatBotGenre, ReadChatBotGenreViewModel>
    {
    }
}
