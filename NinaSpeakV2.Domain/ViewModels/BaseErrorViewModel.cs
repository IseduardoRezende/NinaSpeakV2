using NinaSpeakV2.Domain.Interfaces;
using NinaSpeakV2.Domain.Models;
using System.Text.Json.Serialization;

namespace NinaSpeakV2.Domain.ViewModels
{
    public abstract class BaseErrorViewModel : IBaseErrorViewModel
    {
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public IEnumerable<BaseError>? BaseErrors { get; init; }
    }
}
