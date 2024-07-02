using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using NinaSpeakV2.Domain.ViewModels.Users;
using System.Security.Claims;

namespace NinaSpeakV2.Api.Configurations
{
    public static class EnvironmentConfiguration
    {
        public static async Task ConfigureLogin(HttpContext httpContext, ReadUserViewModel user)
        {
            ArgumentNullException.ThrowIfNull(httpContext, nameof(httpContext));
            ArgumentNullException.ThrowIfNull(user, nameof(user));

            var claims = new List<Claim>()
            {
                new Claim("Id", user.Id.ToString()!),
                new Claim("Email", user.Email),
            };

            var clamsIdentity = new ClaimsIdentity(claims,
                CookieAuthenticationDefaults.AuthenticationScheme);

            var properties = new AuthenticationProperties()
            {
                AllowRefresh = true,
                IsPersistent = true
            };

            await httpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
                new ClaimsPrincipal(clamsIdentity), properties);
        }

        public static async Task ConfigureLogout(HttpContext httpContext)
        {
            ArgumentNullException.ThrowIfNull(httpContext, nameof(httpContext));
            await httpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        }
    }
}
