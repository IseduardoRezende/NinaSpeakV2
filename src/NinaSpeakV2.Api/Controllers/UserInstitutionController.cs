using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;
using NinaSpeakV2.Api.Configurations;
using NinaSpeakV2.Api.Enums;
using NinaSpeakV2.Api.Extensions;
using NinaSpeakV2.Api.RequestValidators;
using NinaSpeakV2.Api.Utils;
using NinaSpeakV2.Data.Entities;
using NinaSpeakV2.Domain.Extensions;
using NinaSpeakV2.Domain.Models;
using NinaSpeakV2.Domain.Services.IServices;
using NinaSpeakV2.Domain.Validators;
using NinaSpeakV2.Domain.ViewModels.Institutions;
using NinaSpeakV2.Domain.ViewModels.Users;
using NinaSpeakV2.Domain.ViewModels.UsersInstitutions;

namespace NinaSpeakV2.Api.Controllers
{
    public class UserInstitutionController : 
                 BaseController<UserInstitution, CreateUserInstitutionViewModel, UpdateUserInstitutionViewModel, ReadUserInstitutionViewModel>
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

        [HttpGet("Create/{institutionCode?}"), AllowAnonymous, EnableRateLimiting(nameof(PolicyType.Unauthenticated))]
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
        
        [HttpPost("CreateUI"), ValidateAntiForgeryToken, AllowAnonymous, EnableRateLimiting(nameof(PolicyType.Unauthenticated))]
        public override async Task<IActionResult> Create(CreateUserInstitutionViewModel createModel)
        {            
            var userInstitution = await _userInstitutionService.CreateAsync(createModel);

            if (userInstitution.HasErrors())
            {
                ViewData.SetBaseErrors(userInstitution.BaseErrors!);
                ViewData.TemporarilyStore();
                return RedirectToAction("Create", "UserInstitution", new { institutionCode = createModel.InstitutionCode });
            }
            
            await EnvironmentConfiguration.ConfigureLogin(HttpContext, new ReadUserViewModel { Id = userInstitution.UserFk, Email = userInstitution.UserEmail });
            return RedirectToAction("Index", "Home");
        }
        
        [HttpPost("CreateNew"), ValidateAntiForgeryToken, AllowAnonymous, EnableRateLimiting(nameof(PolicyType.Unauthenticated))]
        public async Task<IActionResult> CreateNew(CreateUserInstitutionViewModel createModel)
        {
            var user = await _loginService.RegisterAsync(createModel);

            if (user.HasErrors())
            {
                ViewData.SetBaseErrors(user.BaseErrors!);
                ViewData.TemporarilyStore();
                return RedirectToAction("Create", "UserInstitution", new { institutionCode = createModel.InstitutionCode });
            }

            await EnvironmentConfiguration.ConfigureLogin(HttpContext, user);
            return RedirectToAction("Index", "Home");  //Redirect To Verification Login View
        }

        public override async Task<IActionResult> Edit(long? institutionId)
        {
            var usersInstitution = await _userInstitutionService.GetMembersByInstitutionFkAsync(institutionId ?? 0);

            if (!BaseValidator.IsValid(usersInstitution))
                return NotFound();

            if (!UserInstitutionRequestValidator.IsOwner(User.GetCurrentUserEmail(), usersInstitution))
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
                ViewData.SetBaseErrors(valueError.BaseErrors!);
                return View(updateModels);
            }

            return RedirectToAction("Edit", "UserInstitution", new { InstitutionId = updateModels.First().InstitutionFk } );
        }        

        [HttpPost("Delete"), ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(long userId, long institutionId)
        {
            if (!UserRequestValidator.IsHimself(userId, User))
                return Forbid();

            if (!await _userInstitutionService.SoftDeleteAsync(userId, institutionId))
                return NotFound();

            return RedirectToAction("Index", "Home");
        }        

        [HttpPost("OwnerDelete"), ValidateAntiForgeryToken]
        public async Task<IActionResult> OwnerDelete(long userId, long institutionId)
        {
            if (!await _userInstitutionService.SoftDeleteAsync(userId, institutionId))
                return NotFound();

            return RedirectToAction("Edit", "UserInstitution", new { InstitutionId = institutionId });
        }
    }
}
