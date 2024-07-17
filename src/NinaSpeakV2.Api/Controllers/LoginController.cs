using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NinaSpeakV2.Api.Extensions;
using NinaSpeakV2.Domain.Services.IServices;
using NinaSpeakV2.Domain.ViewModels.Login;
using NinaSpeakV2.Domain.Extensions;
using NinaSpeakV2.Api.Configurations;
using Microsoft.AspNetCore.RateLimiting;
using NinaSpeakV2.Api.Enums;

namespace NinaSpeakV2.Api.Controllers
{
    [Route("[controller]"), AllowAnonymous, EnableRateLimiting(nameof(PolicyType.Unauthenticated))]
    public class LoginController : Controller
    {
        private readonly ILoginService _loginService;

        public LoginController(ILoginService loginService)
        {
            _loginService = loginService;
        }

        [HttpGet("Index")]
        public IActionResult Index()
        {
            if (!HttpContext.IsLogged())
                return View();

            return RedirectToAction("Index", "Home");
        }

        [HttpPost("Index"), ValidateAntiForgeryToken]
        public async Task<IActionResult> Index(ReadLoginViewModel login)
        {
            var user = await _loginService.LoginAsync(login);

            if (user.HasErrors())
            {
                ViewData.SetBaseErrors(user.BaseErrors!);
                return View(login);
            }

            await EnvironmentConfiguration.ConfigureLogin(HttpContext, user);
            return RedirectToAction("Index", "Home");
        }

        [HttpGet("Create")]
        public IActionResult Create()
        {
            return View();
        }
        
        [HttpPost("Create"), ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateLoginViewModel login)
        {
            var user = await _loginService.RegisterAsync(login);

            if (user.HasErrors())
            {
                ViewData.SetBaseErrors(user.BaseErrors!);
                return View(login);
            }

            return RedirectToAction("Index");
        }
    }
}
