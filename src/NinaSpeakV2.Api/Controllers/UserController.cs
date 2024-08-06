using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using NinaSpeakV2.Api.Configurations;
using NinaSpeakV2.Api.RequestValidators;
using NinaSpeakV2.Data.Entities;
using NinaSpeakV2.Domain.Services.IServices;
using NinaSpeakV2.Domain.ViewModels.Users;

namespace NinaSpeakV2.Api.Controllers
{    
    public class UserController : BaseController<User, CreateUserViewModel, UpdateUserViewModel, ReadUserViewModel>
    {
        public UserController(IUserService userService, IMapper mapper) : base(userService, mapper)
        { }

        public override async Task<IActionResult> Details(long? id)
        {
            if (!UserRequestValidator.IsHimself(id, User))
                return Forbid();

            return await base.Details(id);
        }

        public override async Task<IActionResult> Delete(long? id)
        {
            if (!UserRequestValidator.IsHimself(id, User))
                return Forbid();

            return await base.Delete(id);
        }

        public override async Task<IActionResult> Delete(long id)
        {
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
    }
}
