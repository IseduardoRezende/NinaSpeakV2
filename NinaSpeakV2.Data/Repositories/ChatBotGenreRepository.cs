using NinaSpeakV2.Data.Models;
using NinaSpeakV2.Data.Repositories.IRepositories;

namespace NinaSpeakV2.Data.Repositories
{
    public class ChatBotGenreRepository : BaseReadonlyRepository<ChatBotGenre>, IChatBotGenreRepository
    {
        public ChatBotGenreRepository(NinaSpeakContext context) : base(context) { }

        public override async Task<IEnumerable<ChatBotGenre>> GetAsync(Func<ChatBotGenre, bool> filters, params string[] includes)
        {
            var chatBotGenres = await base.GetAsync(filters, includes);
            return chatBotGenres.OrderBy(c => c.Description);
        }
    }
}
