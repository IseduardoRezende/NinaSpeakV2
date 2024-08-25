using NinaSpeakV2.Data.Entities;
using NinaSpeakV2.Domain.ViewModels.ChatBotTypes;

namespace NinaSpeakV2.Domain.Services.IServices
{
    public interface IChatBotTypeService : IBaseReadonlyService<ChatBotType, ReadChatBotTypeViewModel>
    {
    }
}
