﻿using NinaSpeakV2.Api.Enums;
using System.Security.Claims;

namespace NinaSpeakV2.Api.Extensions
{
    public static class ClaimsExtensions
    {
        public static bool IsLogged(this ClaimsPrincipal claimsPrincipal)
        {
            ArgumentNullException.ThrowIfNull(claimsPrincipal, nameof(claimsPrincipal));
            return claimsPrincipal?.Identity?.IsAuthenticated ?? false;
        }

        public static string GetClaimValueByType(this ClaimsPrincipal claimsPrincipal, ClaimsType type)
        {
            ArgumentNullException.ThrowIfNull(claimsPrincipal, nameof(claimsPrincipal));

            var typeValue = Enum.GetName(type);
            return claimsPrincipal.Claims.First(c => c.Type.Equals(typeValue, StringComparison.InvariantCultureIgnoreCase)).Value;
        }

        public static long GetCurrentUserId(this ClaimsPrincipal claimsPrincipal)
        {
            ArgumentNullException.ThrowIfNull(claimsPrincipal, nameof(claimsPrincipal));
            return Convert.ToInt64(GetClaimValueByType(claimsPrincipal, ClaimsType.Id));
        }

        public static string GetCurrentUserEmail(this ClaimsPrincipal claimsPrincipal)
        {
            ArgumentNullException.ThrowIfNull(claimsPrincipal, nameof(claimsPrincipal));
            return GetClaimValueByType(claimsPrincipal, ClaimsType.Email);
        }       
    }
}
