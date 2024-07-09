using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using NinaSpeakV2.Api.Extensions;
using NinaSpeakV2.Data.Models;
using NinaSpeakV2.Domain.Extensions;
using NinaSpeakV2.Domain.Services.IServices;
using NinaSpeakV2.Domain.Validators;
using NinaSpeakV2.Domain.ViewModels.UsersInstitutions;

namespace NinaSpeakV2.Api.Controllers
{
    public class UserInstitutionController
        : BaseController<UserInstitution, CreateUserInstitutionViewModel, UpdateUserInstitutionViewModel, ReadUserInstitutionViewModel>
    {
        private readonly IUserInstitutionService _userInstitutionService;

        public UserInstitutionController(IUserInstitutionService userInstitutionService, IMapper mapper) 
            : base(userInstitutionService, mapper)
        {
            _userInstitutionService = userInstitutionService;
        }

        public override async Task<IActionResult> Index()
        {
            return View(await _readonlyService.GetAsync("User", "Institution"));
        }        

        public override async Task<IActionResult> Edit(long? institutionId)
        {
            var usersInstitution = await _userInstitutionService.GetMembersByInstitutionFkAsync(institutionId ?? 0);

            if (!BaseValidator.IsValid(usersInstitution))
                return NotFound();

            var updateModel = _mapper.Map<IEnumerable<UpdateUserInstitutionViewModel>>(usersInstitution);
            return View(updateModel);
        }        

        [HttpPost("Edit"), ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(IEnumerable<UpdateUserInstitutionViewModel> updateModels)
        {
            if (!BaseValidator.IsValid(updateModels))
                return BadRequest();

            var values = await _userInstitutionService.UpdateAsync(updateModels);

            var valueError = values.FirstOrDefault(ui => ui.HasErrors());

            if (valueError is not null)
            {
                ViewData = ViewData.SetBaseErrors(valueError.BaseErrors!);
                return View(updateModels);
            }

            return RedirectToAction("Edit", "Institution", new { Id = updateModels.First().InstitutionFk } );
        }

        [HttpGet("Delete/{userId?}/{institutionId?}")]
        public async Task<IActionResult> Delete(long? userId, long? institutionId)
        {
            var userInstitution = await _readonlyService.GetByIdsAsync(new[] { userId ?? 0, institutionId ?? 0 }, "User", "Institution");

            if (!BaseValidator.IsValid(userInstitution))
                return NotFound();

            var updateModel = _mapper.Map<ReadUserInstitutionViewModel>(userInstitution);
            return View(updateModel);
        }
    }
}
