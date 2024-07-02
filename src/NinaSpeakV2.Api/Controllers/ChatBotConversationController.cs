using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using NinaSpeakV2.Api.Extensions;
using NinaSpeakV2.Api.Utils;
using NinaSpeakV2.Data.Models;
using NinaSpeakV2.Domain.Services.IServices;
using NinaSpeakV2.Domain.ViewModels.ChatBotConversations;

namespace NinaSpeakV2.Api.Controllers
{
    public class ChatBotConversationController
        : BaseController<ChatBotConversation, CreateChatBotConversationViewModel, UpdateChatBotConversationViewModel, ReadChatBotConversationViewModel>
    {
        private readonly IChatBotService _chatBotService;

        public ChatBotConversationController(IChatBotConversationService chatBotConversationService, IChatBotService chatBotService, 
                                             IMapper mapper) : base(chatBotConversationService, mapper)
        { 
            _chatBotService = chatBotService;
        }

        public override async Task<IActionResult> Index()
        {
            return View(await _readonlyService.GetAsync("ChatBot"));
        }

        public override async Task<IActionResult> Create()
        {
            var userId = User.GetCurrentUserId();
            ViewData[Constant.ViewDataChatBots] = await _chatBotService.GetByUserIdAsync(userId);
            return View();
        }        
    }
}
