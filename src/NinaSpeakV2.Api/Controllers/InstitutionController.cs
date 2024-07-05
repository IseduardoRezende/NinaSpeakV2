using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using NinaSpeakV2.Api.Extensions;
using NinaSpeakV2.Data.Models;
using NinaSpeakV2.Domain.Entities;
using NinaSpeakV2.Domain.Services.IServices;
using NinaSpeakV2.Domain.Validators;
using NinaSpeakV2.Domain.ViewModels.Institutions;

namespace NinaSpeakV2.Api.Controllers
{
    public class InstitutionController
        : BaseController<Institution, CreateInstitutionViewModel, UpdateInstitutionViewModel, ReadInstitutionViewModel>
    {
        private readonly IUserInstitutionService _userInstitutionService;

        public InstitutionController(IInstitutionService institutionService, IUserInstitutionService userInstitutionService,
                                     IMapper mapper) : base(institutionService, mapper)
        {
            _userInstitutionService = userInstitutionService;
        }

        public override async Task<IActionResult> Index()
        {
            var userId = User.GetCurrentUserId();
            return View(await _userInstitutionService.GetByUserFkAsync(userId));
        }

        public override async Task<IActionResult> Details(long? id)
        {
            var institution = await _readonlyService.GetByIdAsync(id ?? 0);

            if (institution is null)
                return NotFound();

            var members = await _userInstitutionService.GetMembersByInstitutionFkAsync((long)institution.Id!);
            institution.Members = members.OrderBy(ui => ui.UserEmail);
            
            return View(institution);
        }

        public override async Task<IActionResult> Create(CreateInstitutionViewModel createModel)
        {
            if (!BaseValidator.IsValid(createModel))
            {
                ViewData = ViewData.SetBaseErrors([new BaseError(BaseError.NullObject)]);
                return View();
            }

            createModel.UserFk = User.GetCurrentUserId();
            return await base.Create(createModel);
        }

        public override async Task<IActionResult> Edit(long? id)
        {
            var institution = await _readonlyService.GetByIdAsync(id ?? 0);

            if (institution is null)
                return NotFound();

            var members = await _userInstitutionService.GetMembersByInstitutionFkAsync((long)institution.Id!);
            
            var updateModel = _mapper.Map<UpdateInstitutionViewModel>(institution);           
            updateModel.Members = members.OrderBy(ui => ui.UserEmail);

            return View(updateModel);
        }
    }
}
