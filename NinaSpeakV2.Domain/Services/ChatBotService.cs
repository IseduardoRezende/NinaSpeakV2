﻿using AutoMapper;
using NinaSpeakV2.Data.Entities;
using NinaSpeakV2.Data.Repositories.IRepositories;
using NinaSpeakV2.Domain.Models;
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
        private readonly IChatBotTypeService _chatBotTypeService;
        private readonly IChatBotGenreService _chatBotGenreService;

        public ChatBotService(IChatBotRepository chatBotRepository, IInstitutionService institutionService,
                              IChatBotGenreService chatBotGenreService, IChatBotTypeService chatBotTypeService, 
                              IMapper mapper) : base(chatBotRepository, mapper)
        {
            _chatBotRepository = chatBotRepository;
            _institutionService = institutionService;
            _chatBotTypeService = chatBotTypeService;
            _chatBotGenreService = chatBotGenreService;
        }       

        protected override Func<ChatBot, bool> ApplyFilters()
        {
            return _ => true;
        }

        protected override async Task<IEnumerable<BaseError>> ValidateCreationAsync(CreateChatBotViewModel createViewModel)
        {
            var errors = new List<BaseError>();

            if (!BaseValidator.IsValid(createViewModel))
            {
                errors.Add(new BaseError(BaseError.NullObject));
                return errors;
            }

            var chatBotGenre = await _chatBotGenreService.GetByIdAsync(createViewModel.ChatBotGenreFk);

            if (!BaseValidator.IsValid(chatBotGenre))
            {
                errors.Add(new BaseError(BaseError.ChatBotGenreNotFound));
                return errors;
            }

            var chatBotType = await _chatBotTypeService.GetByIdAsync(createViewModel.ChatBotTypeFk);

            if (!BaseValidator.IsValid(chatBotType))
            {
                errors.Add(new BaseError(BaseError.ChatBotTypeNotFound));
                return errors;
            }

            var institution = await _institutionService.GetByIdAsync(createViewModel.InstitutionFk);

            if (!BaseValidator.IsValid(institution))
            {
                errors.Add(new BaseError(BaseError.InstitutionNotFound));
                return errors;
            }

            if (!BaseEnumValidator.IsValidDescription(createViewModel.Description))
                errors.Add(new BaseError(BaseError.InvalidDescription));

            if (!ChatBotValidator.IsValidName(createViewModel.Name))
                errors.Add(new BaseError(BaseError.InvalidName));

            if (await _chatBotRepository.NameAlreadyExistAsync((long)institution!.Id!, createViewModel.Name))
                errors.Add(new BaseError(BaseError.NameAlreadyExist));

            return errors;
        }

        protected override async Task<IEnumerable<BaseError>> ValidateChangeAsync(UpdateChatBotViewModel updateViewModel)
        {
            var errors = new List<BaseError>();

            if (!BaseValidator.IsValid(updateViewModel))
            {
                errors.Add(new BaseError(BaseError.NullObject));
                return errors;
            }

            var chatBot = await _chatBotRepository.GetByIdAsync(updateViewModel.Id);

            if (!BaseValidator.IsValid(chatBot))
            {
                errors.Add(new BaseError(BaseError.ChatBotNotFound));
                return errors;
            }

            if (!BaseEnumValidator.IsValidDescription(updateViewModel.Description))
                errors.Add(new BaseError(BaseError.InvalidDescription));

            if (!ChatBotValidator.IsValidName(updateViewModel.Name))
                errors.Add(new BaseError(BaseError.InvalidName));

            if (ChatBotValidator.IsEqual(chatBot!, updateViewModel))  //Create an abstract implementation (IsEqual) foreach [Model]Validator
                errors.Add(new BaseError(BaseError.NoChangesDetected));

            var chatBotGenre = await _chatBotGenreService.GetByIdAsync(updateViewModel.ChatBotGenreFk);

            if (!BaseValidator.IsValid(chatBotGenre))
            {
                errors.Add(new BaseError(BaseError.ChatBotGenreNotFound));
                return errors;
            }

            var chatBotType = await _chatBotTypeService.GetByIdAsync(updateViewModel.ChatBotTypeFk);

            if (!BaseValidator.IsValid(chatBotType))
            {
                errors.Add(new BaseError(BaseError.ChatBotTypeNotFound));
                return errors;
            }

            var institution = await _institutionService.GetByIdAsync(chatBot!.InstitutionFk);

            if (!BaseValidator.IsValid(institution))
            {
                errors.Add(new BaseError(BaseError.InstitutionNotFound));
                return errors;
            }

            if (!await _chatBotRepository.CanChangeNameAsync(chatBot!, (long)institution!.Id!, updateViewModel.Name))
                errors.Add(new BaseError(BaseError.NameAlreadyExist));

            return errors;
        }       

        public async Task<IEnumerable<ReadChatBotViewModel>> GetByInstitutionsFksAsync(IEnumerable<long> institutionsFks)
        {
            var chatBots = await _chatBotRepository.GetByInstitutionsFks(institutionsFks);

            if (!BaseValidator.IsValid(chatBots))
                return Enumerable.Empty<ReadChatBotViewModel>();

            return _mapper.Map<IEnumerable<ReadChatBotViewModel>>(chatBots);
        }

        protected override void UpdateFields(ChatBot entity, UpdateChatBotViewModel updateViewModel)
        {
            entity.Name = updateViewModel.Name;
            entity.Description = updateViewModel.Description;
            entity.ChatBotGenreFk = updateViewModel.ChatBotGenreFk;
            entity.ChatBotTypeFk = updateViewModel.ChatBotTypeFk;
        }
    }
}
