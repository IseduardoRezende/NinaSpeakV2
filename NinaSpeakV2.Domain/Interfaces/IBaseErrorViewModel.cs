using NinaSpeakV2.Domain.Entities;

namespace NinaSpeakV2.Domain.Interfaces
{
    public interface IBaseErrorViewModel
    {
        public IEnumerable<BaseError>? BaseErrors { get; init; }
    }
}
