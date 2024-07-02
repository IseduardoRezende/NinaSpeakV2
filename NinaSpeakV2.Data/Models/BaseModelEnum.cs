using NinaSpeakV2.Data.Interfaces;

namespace NinaSpeakV2.Data.Models
{
    public abstract class BaseModelEnum : BaseModel, IBaseModelEnum
    {
        public string? Description { get; set; }
    }
}
