using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NinaSpeakV2.Api.Configurations;
using NinaSpeakV2.Api.Temporal;
using System.Diagnostics;

namespace NinaSpeakV2.Api.Controllers
{
    [Route("[controller]"), Authorize]
    public class HomeController : Controller
    {
        [HttpGet("Index")]
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet("Privacy")]
        public IActionResult Privacy()
        {
            return View();
        }

        [HttpGet("Logout")]
        public async Task<IActionResult> Logout()
        {
            await EnvironmentConfiguration.ConfigureLogout(HttpContext);
            return RedirectToAction("Index", "Login");
        }

        [HttpGet("Error"), 
        ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
