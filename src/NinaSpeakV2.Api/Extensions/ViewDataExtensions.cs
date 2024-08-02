using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.Extensions.Caching.Memory;
using NinaSpeakV2.Api.Utils;
using NinaSpeakV2.Domain.Models;
using System.Security.Claims;

namespace NinaSpeakV2.Api.Extensions
{
    public static class ViewDataExtensions
    {
        private readonly static IMemoryCache _viewDataCache = new MemoryCache(new MemoryCacheOptions());

        public static void TemporarilyStore(this ViewDataDictionary viewData)
        {
            ArgumentNullException.ThrowIfNull(viewData, nameof(viewData));

            lock (viewData)
            {
                _viewDataCache.Set(viewData.Last().Key, viewData.Last().Value, new MemoryCacheEntryOptions
                {
                    AbsoluteExpirationRelativeToNow = TimeSpan.FromSeconds(5)
                });
            }
        }

        public static bool TryGetValues(this ViewDataDictionary viewData, object key, out object? value)
        {
            ArgumentNullException.ThrowIfNull(viewData, nameof(viewData));
            ArgumentNullException.ThrowIfNull(key, nameof(key));

            return _viewDataCache.TryGetValue(key, out value);
        }

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
