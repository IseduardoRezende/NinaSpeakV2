using NinaSpeakV2.Data.Interfaces;

namespace NinaSpeakV2.Data.Entities
{
    public abstract class BaseEntityGlobal : IBaseEntityGlobal
    {
        public DateTime CreatedAt { get; set; }

        public DateTime? DeletedAt { get; set; }
    }
}
