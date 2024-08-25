using AutoMapper;
using NinaSpeakV2.Data.Entities;
using NinaSpeakV2.Domain.ViewModels.ChatBotUsersInstitutions;

namespace NinaSpeakV2.Domain.Profiles
{
    public class ChatBotUserInstitutionProfile : Profile
    {
        public ChatBotUserInstitutionProfile()
        {
            CreateMap<CreateChatBotUserInstitutionViewModel, ChatBotUserInstitution>();
            CreateMap<UpdateChatBotUserInstitutionViewModel, ChatBotUserInstitution>();
            CreateMap<ReadChatBotUserInstitutionViewModel, UpdateChatBotUserInstitutionViewModel>();
            CreateMap<ChatBotUserInstitution, ReadChatBotUserInstitutionViewModel>();
        }
    }
}
