using NinaSpeakV2.Domain.Interfaces;
using System.Text.Json.Serialization;

namespace NinaSpeakV2.Domain.ViewModels
{
    public abstract class BaseUpdateViewModel : BaseGlobalViewModel, IBaseUpdateViewModel
    {
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public long Id { get; set; }
    }
}
