namespace NinaSpeakV2.Api.Extensions
{
    public static class HttpContextExtensions
    {
        public static bool IsLogged(this HttpContext httpContext)
        {
            ArgumentNullException.ThrowIfNull(httpContext, nameof(httpContext));
            return httpContext?.User?.Identity?.IsAuthenticated ?? false;
        }
    }
}
