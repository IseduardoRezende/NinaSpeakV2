using NinaSpeakV2.Domain.Interfaces;
using System.Text.Json.Serialization;

namespace NinaSpeakV2.Domain.ViewModels
{
    public abstract class BaseReadViewModel : BaseGlobalViewModel, IBaseReadViewModel
    {
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public long? Id { get; set; }
    }
}
