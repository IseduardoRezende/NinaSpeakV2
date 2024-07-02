using AutoMapper;
using NinaSpeakV2.Data.Models;
using NinaSpeakV2.Data.Repositories.IRepositories;
using NinaSpeakV2.Domain.Entities;
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

        protected override async Task<IEnumerable<BaseError>> ValidateCreationAsync(CreateChatBotConversationViewModel createModel)
        {
            var errors = new List<BaseError>();

            if (!BaseValidator.IsValid(createModel))
            {
                errors.Add(new BaseError(BaseError.NullObject));
                return errors;
            }

            var chatBot = await _chatBotService.GetByIdAsync(createModel.ChatBotFk);

            if (!BaseValidator.IsValid(chatBot))
                errors.Add(new BaseError(BaseError.ChatBotNotFound));

            if (!ChatBotConversationValidator.IsValidMessage(createModel.Message))
                errors.Add(new BaseError(BaseError.InvalidMessage));

            if (!ChatBotConversationValidator.IsValidResponse(createModel.Response))
                errors.Add(new BaseError(BaseError.InvalidResponse));

            return errors;
        }

        protected override async Task<IEnumerable<BaseError>> ValidateChangeAsync(UpdateChatBotConversationViewModel updateModel)
        {
            var errors = new List<BaseError>();

            if (!BaseValidator.IsValid(updateModel))
            {
                errors.Add(new BaseError(BaseError.NullObject));
                return errors;
            }

            var model = await _baseReadonlyRepository.GetByIdAsync(updateModel.Id);

            if (!BaseValidator.IsValid(model))
            {
                errors.Add(new BaseError(BaseError.ChatBotConversationNotFound));
                return errors;
            }

            if (ChatBotConversationValidator.IsEqual(model!, updateModel))
                errors.Add(new BaseError(BaseError.NoChangesDetected));
            
            if (!ChatBotConversationValidator.IsValidMessage(updateModel.Message))
                errors.Add(new BaseError(BaseError.InvalidMessage));

            if (!ChatBotConversationValidator.IsValidResponse(updateModel.Response))
                errors.Add(new BaseError(BaseError.InvalidResponse));

            return errors;
        }

        protected override void UpdateFields(ChatBotConversation model, UpdateChatBotConversationViewModel updateModel)
        {
            model.Message = updateModel.Message;
            model.Response = updateModel.Response;
        }
    }
}
