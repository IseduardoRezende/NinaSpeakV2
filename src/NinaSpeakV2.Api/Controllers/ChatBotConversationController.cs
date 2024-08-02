using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using NinaSpeakV2.Api.Extensions;
using NinaSpeakV2.Api.Utils;
using NinaSpeakV2.Data.Entities;
using NinaSpeakV2.Domain.Services.IServices;
using NinaSpeakV2.Domain.ViewModels.ChatBotConversations;

namespace NinaSpeakV2.Api.Controllers
{
    public class ChatBotConversationController
        : BaseController<ChatBotConversation, CreateChatBotConversationViewModel, UpdateChatBotConversationViewModel, ReadChatBotConversationViewModel>
    {
        private readonly IUserInstitutionService _userInstitutionService;

        public ChatBotConversationController(IChatBotConversationService chatBotConversationService, IUserInstitutionService userInstitutionService, 
                                             IMapper mapper) : base(chatBotConversationService, mapper)
        {
            _userInstitutionService = userInstitutionService;
        }

        public override async Task<IActionResult> Index()
        {
            return View(await _readonlyService.GetAsync("ChatBot"));
        }

        public override async Task<IActionResult> Create()
        {
            var userId = User.GetCurrentUserId();
            ViewData[Constant.ViewDataChatBots] = await _userInstitutionService.GetByUserFkAsync(userId, onlyWriter: true);
            return View();
        }        
    }
}
