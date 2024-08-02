using NinaSpeakV2.Data.Interfaces;

namespace NinaSpeakV2.Data.Entities
{
    public abstract class BaseEntityEnum : BaseEntity, IBaseEntityEnum
    {
        public string? Description { get; set; }
    }
}
