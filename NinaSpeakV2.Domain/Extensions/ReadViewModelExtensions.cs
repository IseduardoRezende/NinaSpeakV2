using NinaSpeakV2.Domain.Interfaces;

namespace NinaSpeakV2.Domain.Extensions
{
    public static class ReadViewModelExtensions
    {
        public static bool HasErrors(this IBaseReadViewModel readModel)
        {
            ArgumentNullException.ThrowIfNull(readModel, nameof(readModel));            
            return readModel.BaseErrors?.Any() ?? false;
        }
    }
}
