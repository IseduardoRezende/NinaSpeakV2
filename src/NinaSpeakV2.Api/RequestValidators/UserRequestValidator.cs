using NinaSpeakV2.Api.Enums;
using NinaSpeakV2.Api.Extensions;
using System.Security.Claims;

namespace NinaSpeakV2.Api.RequestValidators
{
    public class UserRequestValidator
    {
        public static bool IsHimself(long? userId, ClaimsPrincipal claimsPrincipal)
        {
            ArgumentNullException.ThrowIfNull(claimsPrincipal, nameof(claimsPrincipal));
            return Convert.ToInt64(claimsPrincipal.GetClaimValueByType(ClaimsType.Id)) == (userId ?? 0);
        }
    }
}
