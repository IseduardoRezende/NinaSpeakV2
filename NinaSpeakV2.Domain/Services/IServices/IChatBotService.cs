using NinaSpeakV2.Data.Entities;
using NinaSpeakV2.Domain.ViewModels.ChatBots;

namespace NinaSpeakV2.Domain.Services.IServices
{
    public interface IChatBotService 
        : IBaseService<ChatBot, CreateChatBotViewModel, UpdateChatBotViewModel, ReadChatBotViewModel>
    {
        Task<IEnumerable<ReadChatBotViewModel>> GetByInstitutionsFksAsync(IEnumerable<long> institutionsFks);
    }
}
