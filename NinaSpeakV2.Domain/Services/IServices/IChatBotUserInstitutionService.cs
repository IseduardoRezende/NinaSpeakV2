using NinaSpeakV2.Data.Entities;
using NinaSpeakV2.Domain.ViewModels.ChatBotUsersInstitutions;

namespace NinaSpeakV2.Domain.Services.IServices
{
    public interface IChatBotUserInstitutionService :
        IBaseService<ChatBotUserInstitution, CreateChatBotUserInstitutionViewModel, UpdateChatBotUserInstitutionViewModel, ReadChatBotUserInstitutionViewModel>
    {
        Task<IEnumerable<ReadChatBotUserInstitutionViewModel>> GetByUserIdAsync(long userId);
    }
}
