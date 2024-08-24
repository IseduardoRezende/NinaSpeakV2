using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using NinaSpeakV2.Api.Configurations;
using NinaSpeakV2.Api.Extensions;
using NinaSpeakV2.Api.RequestValidators;
using NinaSpeakV2.Data.Entities;
using NinaSpeakV2.Domain.Extensions;
using NinaSpeakV2.Domain.Services.IServices;
using NinaSpeakV2.Domain.ViewModels.Users;

namespace NinaSpeakV2.Api.Controllers
{    
    public class UserController : BaseController<User, CreateUserViewModel, UpdateUserViewModel, ReadUserViewModel>
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService, IMapper mapper) : base(userService, mapper)
        { 
            _userService = userService;
        }

        public override async Task<IActionResult> Details(long? id)
        {
            if (!UserRequestValidator.IsHimself(id, User))
                return Forbid();

            return await base.Details(id);
        }

        [HttpPost("Delete"), ValidateAntiForgeryToken]
        public override async Task<IActionResult> Delete(long id)
        {
            if (!UserRequestValidator.IsHimself(id, User))
                return Forbid();

            if (!await _baseService.SoftDeleteAsync(id))
                return NotFound();

            await EnvironmentConfiguration.ConfigureLogout(HttpContext);
            return RedirectToAction("Index", "Login");
        }

        public override async Task<IActionResult> Edit(long? id)
        {
            if (!UserRequestValidator.IsHimself(id, User))
                return Forbid();

            return await base.Edit(id);
        }

        public override async Task<IActionResult> Edit(long id, UpdateUserViewModel updateModel)
        {
            if (id != updateModel.Id)
                return BadRequest();

            var value = await _baseService.UpdateAsync(updateModel);

            if (value.HasErrors())
            {
                ViewData.SetBaseErrors(value.BaseErrors!);
                return View(updateModel);
            }

            await EnvironmentConfiguration.ConfigureLogout(HttpContext);
            return RedirectToAction("Login", "Verification");  //Redirect To Verification View
        }

        [HttpGet("EditPassword/{id?}")]
        public IActionResult EditPassword(long? id)
        {
            if (!UserRequestValidator.IsHimself(id, User))
                return Forbid();

            return View(new UpdateUserPasswordViewModel { Id = (long)id! });
        }

        [HttpPost("EditPassword/{id}"), ValidateAntiForgeryToken]
        public async Task<IActionResult> EditPassword(long id, UpdateUserPasswordViewModel updateUser)
        {
            if (id != updateUser.Id)
                return BadRequest();

            var user = await _userService.UpdatePasswordAsync(updateUser);

            if (user.HasErrors())
            {
                ViewData.SetBaseErrors(user.BaseErrors!);
                return View(updateUser);
            }

            return RedirectToAction("Index", "Home");
        }        
    }
}
