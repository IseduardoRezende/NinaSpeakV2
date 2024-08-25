using AutoMapper;
using NinaSpeakV2.Data.Entities;
using NinaSpeakV2.Data.Repositories.IRepositories;
using NinaSpeakV2.Domain.Models;
using NinaSpeakV2.Domain.Services.IServices;
using NinaSpeakV2.Domain.ViewModels.ChatBotUsersInstitutions;

namespace NinaSpeakV2.Domain.Services
{
    public class ChatBotUserInstitutionService : 
                 BaseService<ChatBotUserInstitution, CreateChatBotUserInstitutionViewModel, UpdateChatBotUserInstitutionViewModel, ReadChatBotUserInstitutionViewModel>,
                 IChatBotUserInstitutionService
    {
        public ChatBotUserInstitutionService(IChatBotUserInstitutionRepository chatBotUserInstitutionRepository, IMapper mapper) 
            : base(chatBotUserInstitutionRepository, mapper)
        { }

        protected override Func<ChatBotUserInstitution, bool> ApplyFilters()
        {
            return _ => true;
        }

        protected override void UpdateFields(ChatBotUserInstitution entity, UpdateChatBotUserInstitutionViewModel updateViewModel)
        {
            throw new NotImplementedException();
        }

        protected override Task<IEnumerable<BaseError>> ValidateChangeAsync(UpdateChatBotUserInstitutionViewModel updateViewModel)
        {
            throw new NotImplementedException();
        }

        protected override Task<IEnumerable<BaseError>> ValidateCreationAsync(CreateChatBotUserInstitutionViewModel createViewModel)
        {
            throw new NotImplementedException();
        }
    }
}
