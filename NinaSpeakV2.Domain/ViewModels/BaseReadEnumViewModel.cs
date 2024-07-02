using NinaSpeakV2.Domain.Interfaces;
using System.Text.Json.Serialization;

namespace NinaSpeakV2.Domain.ViewModels
{
    public abstract class BaseReadEnumViewModel : BaseReadViewModel, IBaseReadEnumViewModel
    {
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string? Description { get; set; }
    }
}
