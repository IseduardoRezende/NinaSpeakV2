using AutoMapper;
using NinaSpeakV2.Data.Models;
using NinaSpeakV2.Data.Repositories.IRepositories;
using NinaSpeakV2.Domain.Entities;
using NinaSpeakV2.Domain.Services.IServices;
using NinaSpeakV2.Domain.Validators;
using NinaSpeakV2.Domain.ViewModels.ChatBots;

namespace NinaSpeakV2.Domain.Services
{
    public class ChatBotService : BaseService<ChatBot, CreateChatBotViewModel, UpdateChatBotViewModel, ReadChatBotViewModel>,
                                  IChatBotService
    {
        private readonly IChatBotRepository _chatBotRepository;
        private readonly IInstitutionService _institutionService;
        private readonly IChatBotGenreService _chatBotGenreService;
        private readonly IUserInstitutionService _userInstitutionService;

        public ChatBotService(IChatBotRepository chatBotRepository, IInstitutionService institutionService,
                              IChatBotGenreService chatBotGenreService, IUserInstitutionService userInstitutionService,
                              IMapper mapper) : base(chatBotRepository, mapper)
        {
            _chatBotRepository = chatBotRepository;
            _institutionService = institutionService;
            _chatBotGenreService = chatBotGenreService;
            _userInstitutionService = userInstitutionService;
        }       

        protected override Func<ChatBot, bool> ApplyFilters()
        {
            return _ => true;
        }

        protected override async Task<IEnumerable<BaseError>> ValidateCreationAsync(CreateChatBotViewModel createModel)
        {
            var errors = new List<BaseError>();

            if (!BaseValidator.IsValid(createModel))
            {
                errors.Add(new BaseError(BaseError.NullObject));
                return errors;
            }

            var chatBotGenre = await _chatBotGenreService.GetByIdAsync(createModel.ChatBotGenreFk);

            if (!BaseValidator.IsValid(chatBotGenre))
            {
                errors.Add(new BaseError(BaseError.ChatBotGenreNotFound));
                return errors;
            }

            var institution = await _institutionService.GetByIdAsync(createModel.InstitutionFk);

            if (!BaseValidator.IsValid(institution))
            {
                errors.Add(new BaseError(BaseError.InstitutionNotFound));
                return errors;
            }

            if (!BaseEnumValidator.IsValidDescription(createModel.Description))
                errors.Add(new BaseError(BaseError.InvalidDescription));

            if (!ChatBotValidator.IsValidName(createModel.Name))
                errors.Add(new BaseError(BaseError.InvalidName));

            if (await _chatBotRepository.NameAlreadyExistAsync((long)institution!.Id!, createModel.Name))
                errors.Add(new BaseError(BaseError.NameAlreadyExist));

            return errors;
        }

        protected override async Task<IEnumerable<BaseError>> ValidateChangeAsync(UpdateChatBotViewModel updateModel)
        {
            var errors = new List<BaseError>();

            if (!BaseValidator.IsValid(updateModel))
            {
                errors.Add(new BaseError(BaseError.NullObject));
                return errors;
            }

            var chatBot = await _chatBotRepository.GetByIdAsync(updateModel.Id);

            if (!BaseValidator.IsValid(chatBot))
            {
                errors.Add(new BaseError(BaseError.ChatBotNotFound));
                return errors;
            }

            if (!BaseEnumValidator.IsValidDescription(updateModel.Description))
                errors.Add(new BaseError(BaseError.InvalidDescription));

            if (!ChatBotValidator.IsValidName(updateModel.Name))
                errors.Add(new BaseError(BaseError.InvalidName));

            if (ChatBotValidator.IsEqual(chatBot!, updateModel))  //Create an abstract implementation (IsEqual) foreach [Model]Validator
                errors.Add(new BaseError(BaseError.NoChangesDetected));

            var chatBotGenre = await _chatBotGenreService.GetByIdAsync(updateModel.ChatBotGenreFk);

            if (!BaseValidator.IsValid(chatBotGenre))
            {
                errors.Add(new BaseError(BaseError.ChatBotGenreNotFound));
                return errors;
            }

            var institution = await _institutionService.GetByIdAsync(chatBot!.InstitutionFk);

            if (!BaseValidator.IsValid(institution))
            {
                errors.Add(new BaseError(BaseError.InstitutionNotFound));
                return errors;
            }

            if (!await _chatBotRepository.CanChangeNameAsync(chatBot!, (long)institution!.Id!, updateModel.Name))
                errors.Add(new BaseError(BaseError.NameAlreadyExist));

            return errors;
        }

        public async Task<IEnumerable<ReadChatBotViewModel>> GetByUserIdAsync(long userId)
        {
            var userInstitutions = await _userInstitutionService.GetByUserFkAsync(userId);

            if (!BaseValidator.IsValid(userInstitutions))
                return Enumerable.Empty<ReadChatBotViewModel>();

            var institutionsFks = userInstitutions.Select(ui => ui.InstitutionFk);

            return await GetByInstitutionsFksAsync(institutionsFks);
        }

        public async Task<IEnumerable<ReadChatBotViewModel>> GetByInstitutionsFksAsync(IEnumerable<long> institutionsFks)
        {
            var chatBots = await _chatBotRepository.GetByInstitutionsFks(institutionsFks);

            if (!BaseValidator.IsValid(chatBots))
                return Enumerable.Empty<ReadChatBotViewModel>();

            return _mapper.Map<IEnumerable<ReadChatBotViewModel>>(chatBots);
        }

        protected override void UpdateFields(ChatBot model, UpdateChatBotViewModel updateModel)
        {
            model.Name = updateModel.Name;
            model.Description = updateModel.Description;
            model.ChatBotGenreFk = updateModel.ChatBotGenreFk;
        }
    }
}
