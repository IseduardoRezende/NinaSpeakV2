using NinaSpeakV2.Data.Interfaces;

namespace NinaSpeakV2.Data.Entities
{
    public abstract class BaseEntity : BaseEntityGlobal, IBaseEntity
    {
        public long Id { get; set; }
    }
}
