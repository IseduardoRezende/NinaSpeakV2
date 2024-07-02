using NinaSpeakV2.Domain.Interfaces;
using System.Text.Json.Serialization;

namespace NinaSpeakV2.Domain.ViewModels
{
    public abstract class BaseCreateEnumViewModel : BaseCreateViewModel, IBaseCreateEnumViewModel
    {
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string? Description { get; set; }
    }
}
