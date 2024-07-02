using NinaSpeakV2.Domain.Entities;
using NinaSpeakV2.Domain.Interfaces;
using System.Text.Json.Serialization;

namespace NinaSpeakV2.Domain.ViewModels
{
    public abstract class BaseErrorViewModel : IBaseErrorViewModel
    {
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public IEnumerable<BaseError>? BaseErrors { get; init; }
    }
}
