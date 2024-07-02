using Microsoft.AspNetCore.Mvc.ViewFeatures;
using NinaSpeakV2.Api.Utils;
using NinaSpeakV2.Domain.Entities;
using System.Security.Claims;

namespace NinaSpeakV2.Api.Extensions
{
    public static class ViewDataExtensions
    {
        public static ViewDataDictionary SetBaseErrors(this ViewDataDictionary viewData, IEnumerable<BaseError> baseErrors)
        {
            ArgumentNullException.ThrowIfNull(viewData, nameof(viewData));
            ArgumentNullException.ThrowIfNull(baseErrors, nameof(baseErrors));

            viewData[Constant.ViewDataBaseErrors] = baseErrors;
            return viewData;
        }

        public static ViewDataDictionary SetCurrentUserId(this ViewDataDictionary viewData, ClaimsPrincipal claimsPrincipal)
        {
            ArgumentNullException.ThrowIfNull(viewData, nameof(viewData));
            ArgumentNullException.ThrowIfNull(claimsPrincipal, nameof(claimsPrincipal));

            viewData[Constant.ViewDataUserId] = claimsPrincipal.GetCurrentUserId();
            return viewData;
        }

        public static ViewDataDictionary SetCurrentUserEmail(this ViewDataDictionary viewData, ClaimsPrincipal claimsPrincipal)
        {
            ArgumentNullException.ThrowIfNull(viewData, nameof(viewData));
            ArgumentNullException.ThrowIfNull(claimsPrincipal, nameof(claimsPrincipal));

            viewData[Constant.ViewDataUserEmail] = claimsPrincipal.GetCurrentUserEmail();
            return viewData;
        }
    }
}
