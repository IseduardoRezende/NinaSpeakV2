using AutoMapper;
using NinaSpeakV2.Data.Entities;
using NinaSpeakV2.Data.Repositories.IRepositories;
using NinaSpeakV2.Domain.Models;
using NinaSpeakV2.Domain.Services.IServices;
using NinaSpeakV2.Domain.Validators;
using NinaSpeakV2.Domain.ViewModels.ChatBotConversations;

namespace NinaSpeakV2.Domain.Services
{
    public class ChatBotConversationService : BaseService<ChatBotConversation, CreateChatBotConversationViewModel, UpdateChatBotConversationViewModel, ReadChatBotConversationViewModel>,
                                              IChatBotConversationService
    {
        private readonly IChatBotService _chatBotService;

        public ChatBotConversationService(IChatBotConversationRepository chatBotConversationRepository, IChatBotService chatBotService,
                                          IMapper mapper) : base(chatBotConversationRepository, mapper)
        {
            _chatBotService = chatBotService;
        }

        protected override Func<ChatBotConversation, bool> ApplyFilters()
        {
            return _ => true;
        }

        protected override async Task<IEnumerable<BaseError>> ValidateCreationAsync(CreateChatBotConversationViewModel createViewModel)
        {
            var errors = new List<BaseError>();

            if (!BaseValidator.IsValid(createViewModel))
            {
                errors.Add(new BaseError(BaseError.NullObject));
                return errors;
            }

            var chatBot = await _chatBotService.GetByIdAsync(createViewModel.ChatBotFk);

            if (!BaseValidator.IsValid(chatBot))
                errors.Add(new BaseError(BaseError.ChatBotNotFound));

            if (!ChatBotConversationValidator.IsValidMessage(createViewModel.Message))
                errors.Add(new BaseError(BaseError.InvalidMessage));

            if (!ChatBotConversationValidator.IsValidResponse(createViewModel.Response))
                errors.Add(new BaseError(BaseError.InvalidResponse));

            return errors;
        }

        protected override async Task<IEnumerable<BaseError>> ValidateChangeAsync(UpdateChatBotConversationViewModel updateViewModel)
        {
            var errors = new List<BaseError>();

            if (!BaseValidator.IsValid(updateViewModel))
            {
                errors.Add(new BaseError(BaseError.NullObject));
                return errors;
            }

            var entity = await _baseReadonlyRepository.GetByIdAsync(updateViewModel.Id);

            if (!BaseValidator.IsValid(entity))
            {
                errors.Add(new BaseError(BaseError.ChatBotConversationNotFound));
                return errors;
            }

            if (ChatBotConversationValidator.IsEqual(entity!, updateViewModel))
                errors.Add(new BaseError(BaseError.NoChangesDetected));
            
            if (!ChatBotConversationValidator.IsValidMessage(updateViewModel.Message))
                errors.Add(new BaseError(BaseError.InvalidMessage));

            if (!ChatBotConversationValidator.IsValidResponse(updateViewModel.Response))
                errors.Add(new BaseError(BaseError.InvalidResponse));

            return errors;
        }

        protected override void UpdateFields(ChatBotConversation entity, UpdateChatBotConversationViewModel updateViewModel)
        {
            entity.Message = updateViewModel.Message;
            entity.Response = updateViewModel.Response;
        }
    }
}
