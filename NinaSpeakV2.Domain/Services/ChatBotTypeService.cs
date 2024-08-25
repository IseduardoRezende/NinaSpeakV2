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

        protected override Func<ChatBotType, bool> ApplyFilters()
        {
            return _ => true;
        }
    }
}
