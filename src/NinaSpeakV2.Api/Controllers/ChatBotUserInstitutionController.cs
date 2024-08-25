using AutoMapper;
using NinaSpeakV2.Data.Entities;
using NinaSpeakV2.Domain.Services.IServices;
using NinaSpeakV2.Domain.ViewModels.ChatBotUsersInstitutions;

namespace NinaSpeakV2.Api.Controllers
{
    public class ChatBotUserInstitutionController :
                 BaseController<ChatBotUserInstitution, CreateChatBotUserInstitutionViewModel, UpdateChatBotUserInstitutionViewModel, ReadChatBotUserInstitutionViewModel>
    {
        public ChatBotUserInstitutionController(IChatBotUserInstitutionService chatBotUserInstitutionService, IMapper mapper) 
            : base(chatBotUserInstitutionService, mapper)
        { }
    }
}
