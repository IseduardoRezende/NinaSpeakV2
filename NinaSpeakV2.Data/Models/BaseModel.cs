using NinaSpeakV2.Data.Interfaces;

namespace NinaSpeakV2.Data.Models
{
    public abstract class BaseModel : BaseModelGlobal, IBaseModel
    {
        public long Id { get; set; }
    }
}
