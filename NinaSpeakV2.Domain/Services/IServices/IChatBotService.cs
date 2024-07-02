using NinaSpeakV2.Data.Models;
using NinaSpeakV2.Domain.ViewModels.ChatBots;

namespace NinaSpeakV2.Domain.Services.IServices
{
    public interface IChatBotService 
        : IBaseService<ChatBot, CreateChatBotViewModel, UpdateChatBotViewModel, ReadChatBotViewModel>
    {
        Task<IEnumerable<ReadChatBotViewModel>> GetByUserIdAsync(long userId);

        Task<IEnumerable<ReadChatBotViewModel>> GetByInstitutionsFksAsync(IEnumerable<long> institutionsFks);
    }
}
