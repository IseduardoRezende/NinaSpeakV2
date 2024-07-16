using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NinaSpeakV2.Api.Extensions;
using NinaSpeakV2.Api.RequestValidators;
using NinaSpeakV2.Api.Utils;
using NinaSpeakV2.Data.Models;
using NinaSpeakV2.Domain.Entities;
using NinaSpeakV2.Domain.Extensions;
using NinaSpeakV2.Domain.Services.IServices;
using NinaSpeakV2.Domain.Validators;
using NinaSpeakV2.Domain.ViewModels.Institutions;
using NinaSpeakV2.Domain.ViewModels.UsersInstitutions;

namespace NinaSpeakV2.Api.Controllers
{
    public class UserInstitutionController
        : BaseController<UserInstitution, CreateUserInstitutionViewModel, UpdateUserInstitutionViewModel, ReadUserInstitutionViewModel>
    {
        private readonly ILoginService _loginService;
        private readonly IInstitutionService _institutionService;
        private readonly IUserInstitutionService _userInstitutionService;

        public UserInstitutionController(IUserInstitutionService userInstitutionService, IInstitutionService institutionService,
                                         ILoginService loginService, IMapper mapper) : base(userInstitutionService, mapper)
        {
            _loginService = loginService;
            _institutionService = institutionService;
            _userInstitutionService = userInstitutionService;
        }

        [HttpGet("Create/{institutionCode?}"), AllowAnonymous]
        public async Task<IActionResult> Create(string? institutionCode)
        {
            if (ViewData.TryGetValues(Constant.ViewDataBaseErrors, out object? values))
            {
                ViewData.SetBaseErrors((values as IEnumerable<BaseError>)!);
            }

            var hasValue = await Task.FromResult(_institutionService.TryGetByCode(institutionCode, out ReadInstitutionViewModel? institution));

            if (!hasValue)
                return NotFound();

            ViewData[Constant.ViewDataInstitution] = institution;
            return View();
        }
        
        [HttpPost("CreateUI"), ValidateAntiForgeryToken, AllowAnonymous]
        public override async Task<IActionResult> Create(CreateUserInstitutionViewModel createModel)
        {            
            var value = await _userInstitutionService.CreateAsync(createModel);

            if (value.HasErrors())
            {
                ViewData.SetBaseErrors(value.BaseErrors!);
                ViewData.StoreValues();
                return RedirectToAction("Create", "UserInstitution", new { institutionCode = createModel.InstitutionCode });
            }

            return RedirectToAction("Index", "Login");
        }

        [HttpPost("CreateNew"), ValidateAntiForgeryToken, AllowAnonymous]
        public async Task<IActionResult> CreateNew(CreateUserInstitutionViewModel createModel)
        {
            var value = await _loginService.RegisterAsync(createModel);

            if (value.HasErrors())
            {
                ViewData.SetBaseErrors(value.BaseErrors!);
                ViewData.StoreValues();
                return RedirectToAction("Create", "UserInstitution", new { institutionCode = createModel.InstitutionCode });
            }

            return RedirectToAction("Index", "Login");
        }

        public override async Task<IActionResult> Edit(long? institutionId)
        {
            var usersInstitution = await _userInstitutionService.GetMembersByInstitutionFkAsync(institutionId ?? 0);

            if (!BaseValidator.IsValid(usersInstitution))
                return NotFound();

            if (!InstitutionRequestValidator.IsOwner(User.GetCurrentUserEmail(), usersInstitution))
                return Forbid();
            
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
            if (!UserRequestValidator.IsHimself(userId, User))
                return Forbid();

            var usersInstitution = await _userInstitutionService.GetMembersByInstitutionFkAsync(institutionId ?? 0);

            if (!BaseValidator.IsValid(usersInstitution))
                return BadRequest();

            if (!InstitutionRequestValidator.IsMember(userId, usersInstitution))
                return Forbid();

            var userInstitution = await _readonlyService.GetByIdsAsync(new[] { userId ?? 0, institutionId ?? 0 }, "User", "Institution");

            if (!BaseValidator.IsValid(userInstitution))
                return NotFound();

            var updateModel = _mapper.Map<ReadUserInstitutionViewModel>(userInstitution);
            return View(updateModel);
        }

        [HttpPost("Delete/{userId}/{institutionId}"), ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(long userId, long institutionId)
        {
            if (!await _userInstitutionService.SoftDeleteAsync(userId, institutionId))
                return NotFound();

            return RedirectToAction("Index", "Home");
        }

        [HttpGet("OwnerDelete/{userId?}/{institutionId?}")]
        public async Task<IActionResult> OwnerDelete(long? userId, long? institutionId)
        {            
            var usersInstitution = await _userInstitutionService.GetMembersByInstitutionFkAsync(institutionId ?? 0);

            if (!BaseValidator.IsValid(usersInstitution))
                return BadRequest();

            if (InstitutionRequestValidator.IsCreator(userId, usersInstitution))
                return Forbid();

            if (!InstitutionRequestValidator.IsOwner(User.GetCurrentUserEmail(), usersInstitution))
                return Forbid();

            var userInstitution = await _readonlyService.GetByIdsAsync(new[] { userId ?? 0, institutionId ?? 0 }, "User", "Institution");

            if (!BaseValidator.IsValid(userInstitution))
                return NotFound();

            var updateModel = _mapper.Map<ReadUserInstitutionViewModel>(userInstitution);
            return View(updateModel);
        }

        [HttpPost("OwnerDelete/{userId}/{institutionId}"), ValidateAntiForgeryToken]
        public async Task<IActionResult> OwnerDelete(long userId, long institutionId)
        {
            if (!await _userInstitutionService.SoftDeleteAsync(userId, institutionId))
                return NotFound();

            return RedirectToAction("Edit", "UserInstitution", new { InstitutionId = institutionId });
        }
    }
}
