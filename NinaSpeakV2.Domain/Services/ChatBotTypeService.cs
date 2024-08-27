using AutoMapper;
using NinaSpeakV2.Data.Entities;
using NinaSpeakV2.Data.Repositories.IRepositories;
using NinaSpeakV2.Domain.Services.IServices;
using NinaSpeakV2.Domain.ViewModels.ChatBotTypes;

namespace NinaSpeakV2.Domain.Services
{
    public class ChatBotTypeService : BaseReadonlyService<ChatBotType, ReadChatBotTypeViewModel>, IChatBotTypeService
    {
        public ChatBotTypeService(IChatBotTypeRepository chatBotTypeRepository, IMapper mapper) 
            : base(chatBotTypeRepository, mapper)
        { }

        public override async Task<IEnumerable<ReadChatBotTypeViewModel>> GetAsync(bool ignoreGlobalFilter = false, params string[] includes)
        {
            var result = await base.GetAsync(ignoreGlobalFilter, includes);
            
            if (result == null || !result.Any())
                return Enumerable.Empty<ReadChatBotTypeViewModel>();

            return result.OrderBy(c => c.Description);            
        }

        protected override Func<ChatBotType, bool> ApplyFilters()
        {
            return _ => true;
        }
    }
}
