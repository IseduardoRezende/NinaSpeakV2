using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using NinaSpeakV2.Api.Extensions;
using NinaSpeakV2.Api.Utils;
using NinaSpeakV2.Data.Entities;
using NinaSpeakV2.Domain.Services.IServices;
using NinaSpeakV2.Domain.ViewModels.ChatBots;

namespace NinaSpeakV2.Api.Controllers
{
    public class ChatBotController
        : BaseController<ChatBot, CreateChatBotViewModel, UpdateChatBotViewModel, ReadChatBotViewModel>
    {
        private readonly IChatBotGenreService _chatBotGenreService;
        private readonly IUserInstitutionService _userInstitutionService;

        //Validate ViewData ViewDataChatBotGenres and ViewDataUserInstitutions on HttpPost Actions to Views

        public ChatBotController(IChatBotService chatBotService, IChatBotGenreService chatBotGenreService,
                                 IUserInstitutionService userInstitutionService, IMapper mapper) 
            : base(chatBotService, mapper)
        {
            _chatBotGenreService = chatBotGenreService;
            _userInstitutionService = userInstitutionService;
        }

        public override async Task<IActionResult> Index()
        {
            return View(await _readonlyService.GetAsync("ChatBotGenre", "Institution"));
        }

        public override async Task<IActionResult> Create()
        {
            var userId = User.GetCurrentUserId();

            ViewData[Constant.ViewDataChatBotGenres] = await _chatBotGenreService.GetAsync();
            ViewData[Constant.ViewDataUserInstitutions] = await _userInstitutionService.GetByOwnerAsync(userId);
            return View();
        }

        public override async Task<IActionResult> Edit(long? id)
        {
            var entity = await _readonlyService.GetByIdAsync(id ?? 0);

            if (entity is null)
                return NotFound();

            var updateModel = _mapper.Map<UpdateChatBotViewModel>(entity);

            ViewData[Constant.ViewDataChatBotGenres] = await _chatBotGenreService.GetAsync();                        
            return View(updateModel);
        }        
    }
}
