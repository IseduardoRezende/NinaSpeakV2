using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using NinaSpeakV2.Api.Extensions;
using NinaSpeakV2.Api.RequestValidators;
using NinaSpeakV2.Api.Utils;
using NinaSpeakV2.Data.Entities;
using NinaSpeakV2.Domain.Extensions;
using NinaSpeakV2.Domain.Models;
using NinaSpeakV2.Domain.Services.IServices;
using NinaSpeakV2.Domain.Validators;
using NinaSpeakV2.Domain.ViewModels.ChatBots;

namespace NinaSpeakV2.Api.Controllers
{
    public class ChatBotController : 
                 BaseController<ChatBot, CreateChatBotViewModel, UpdateChatBotViewModel, ReadChatBotViewModel>
    {
        private readonly IChatBotTypeService _chatBotTypeService;
        private readonly IChatBotGenreService _chatBotGenreService;
        private readonly IUserInstitutionService _userInstitutionService;
        private readonly IChatBotUserInstitutionService _chatBotUserInstitutionService;

        public ChatBotController(IChatBotService chatBotService, IChatBotTypeService chatBotTypeService, IChatBotGenreService chatBotGenreService,
                                 IUserInstitutionService userInstitutionService, IChatBotUserInstitutionService chatBotUserInstitutionService, 
                                 IMapper mapper) : base(chatBotService, mapper)
        {
            _chatBotTypeService = chatBotTypeService;
            _chatBotGenreService = chatBotGenreService;
            _userInstitutionService = userInstitutionService;
            _chatBotUserInstitutionService = chatBotUserInstitutionService;
        }

        public override async Task<IActionResult> Index()
        {
            var userId = User.GetCurrentUserId();
            return View(await _chatBotUserInstitutionService.GetByUserIdAsync(userId));
        }

        public override async Task<IActionResult> Details(long? id)
        {
            var chatBot = await _readonlyService.GetByIdAsync(id ?? 0, "Institution", "ChatBotGenre", "ChatBotType");

            if (!BaseValidator.IsValid(chatBot))
                return NotFound();

            chatBot!.Members = await _chatBotUserInstitutionService.GetMembersByChatBotFkAsync((long)chatBot.Id!);

            if (!ChatBotUserInstitutionRequestValidator.IsMember(User.GetCurrentUserEmail(), chatBot.Members))
                return Forbid();

            return View(chatBot);
        }

        public override async Task<IActionResult> Create()
        {
            if (ViewData.TryGetValues(Constant.ViewDataBaseErrors, out object? values))
            {
                ViewData.SetBaseErrors((values as IEnumerable<BaseError>)!);
            }

            var userId = User.GetCurrentUserId();

            ViewData[Constant.ViewDataChatBotTypes] = await _chatBotTypeService.GetAsync();
            ViewData[Constant.ViewDataChatBotGenres] = await _chatBotGenreService.GetAsync();
            ViewData[Constant.ViewDataUserInstitutions] = await _userInstitutionService.GetByOwnerAsync(userId);
            return View();
        }

        public override async Task<IActionResult> Create(CreateChatBotViewModel createModel)
        {
            var value = await _baseService.CreateAsync(createModel);

            if (value.HasErrors())
            {
                ViewData.SetBaseErrors(value.BaseErrors!);
                ViewData.TemporarilyStore();
                return RedirectToAction("Create");
            }

            return RedirectToAction("Index", "Home");
        }

        public override async Task<IActionResult> Edit(long? id)
        {
            if (ViewData.TryGetValues(Constant.ViewDataBaseErrors, out object? values))
            {
                ViewData.SetBaseErrors((values as IEnumerable<BaseError>)!);
            }

            var entity = await _readonlyService.GetByIdAsync(id ?? 0);

            if (!BaseValidator.IsValid(entity))
                return NotFound();

            var updateModel = _mapper.Map<UpdateChatBotViewModel>(entity);

            var member = await _userInstitutionService.GetByAsync(ui => ui.UserFk == User.GetCurrentUserId() && 
                                                                        ui.InstitutionFk == entity!.InstitutionFk);
            
            if (!BaseValidator.IsValid(member) || 
               (!UserInstitutionRequestValidator.IsCreator(member!) && !UserInstitutionRequestValidator.IsOwner(member!)))
            {
                return Forbid();
            }

            var chatBotGenres = await _chatBotGenreService.GetAsync();
            var chatBotGenreSelectList = new SelectList(chatBotGenres, "Id", "Description", entity!.ChatBotGenreFk);
        
            ViewData[Constant.ViewDataChatBotGenres] = chatBotGenreSelectList;                                    
            return View(updateModel);
        }
        
        public override async Task<IActionResult> Edit(long id, UpdateChatBotViewModel updateModel)
        {
            if (id != updateModel.Id)
                return BadRequest();

            var value = await _baseService.UpdateAsync(updateModel);

            if (value.HasErrors())
            {
                ViewData.SetBaseErrors(value.BaseErrors!);
                ViewData.TemporarilyStore();
                return RedirectToAction("Edit", new { Id = id });
            }

            return RedirectToAction("Index", "Home");
        }

        [HttpPost("Delete"), ValidateAntiForgeryToken]
        public override Task<IActionResult> Delete(long id)
        {
            return base.Delete(id);
        }
    }
}
