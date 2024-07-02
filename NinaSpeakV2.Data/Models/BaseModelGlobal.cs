using NinaSpeakV2.Data.Interfaces;

namespace NinaSpeakV2.Data.Models
{
    public abstract class BaseModelGlobal : IBaseModelGlobal
    {
        public DateTime CreatedAt { get; set; }

        public DateTime? DeletedAt { get; set; }
    }
}
