using AutoMapper;
using NinaSpeakV2.Data.Entities;
using NinaSpeakV2.Data.Repositories.IRepositories;
using NinaSpeakV2.Domain.Models;
using NinaSpeakV2.Domain.Services.IServices;
using NinaSpeakV2.Domain.Validators;
using NinaSpeakV2.Domain.ViewModels.ChatBotUsersInstitutions;

namespace NinaSpeakV2.Domain.Services
{
    public class ChatBotUserInstitutionService : 
                 BaseService<ChatBotUserInstitution, CreateChatBotUserInstitutionViewModel, UpdateChatBotUserInstitutionViewModel, ReadChatBotUserInstitutionViewModel>,
                 IChatBotUserInstitutionService
    {
        private readonly IChatBotRepository _chatBotRepository;
        private readonly IUserInstitutionRepository _userInstitutionRepository;

        public ChatBotUserInstitutionService(IChatBotUserInstitutionRepository chatBotUserInstitutionRepository, 
                                             IChatBotRepository chatBotRepository, IUserInstitutionRepository userInstitutionRepository,
                                             IMapper mapper) : base(chatBotUserInstitutionRepository, mapper)
        { 
            _chatBotRepository = chatBotRepository;
            _userInstitutionRepository = userInstitutionRepository;
        }

        protected override async Task<IEnumerable<BaseError>> ValidateCreationAsync(CreateChatBotUserInstitutionViewModel createViewModel)
        {
            var errors = new List<BaseError>();

            if (!BaseValidator.IsValid(createViewModel))
            {
                errors.Add(new BaseError(BaseError.NullObject));
                return errors;
            }

            var chatBot = await _chatBotRepository.GetByIdAsync(createViewModel.ChatBotFk);

            if (!BaseValidator.IsValid(chatBot))
                errors.Add(new BaseError(BaseError.ChatBotNotFound));

            var userInstitution = await _userInstitutionRepository.GetByIdAsync(createViewModel.UserInstitutionFk);

            if (!BaseValidator.IsValid(userInstitution))
            {
                errors.Add(new BaseError(BaseError.UserInstitutionNotFound));
                return errors;
            }

            if (!UserInstitutionValidator.ContainsLoad(userInstitution!))
                errors.Add(new BaseError(BaseError.UserInstitutionLoad));

            return errors;
        }

        protected override Task<IEnumerable<BaseError>> ValidateChangeAsync(UpdateChatBotUserInstitutionViewModel updateViewModel)
        {
            throw new NotImplementedException();
        }

        protected override Func<ChatBotUserInstitution, bool> ApplyFilters()
        {
            return _ => true;
        }

        protected override void UpdateFields(ChatBotUserInstitution entity, UpdateChatBotUserInstitutionViewModel updateViewModel)
        {
            throw new NotImplementedException();
        }
    }
}
