namespace NinaSpeakV2.Data.Interfaces
{
    public interface IBaseModelGlobal
    {
        public DateTime CreatedAt { get; set; }

        public DateTime? DeletedAt { get; set; }
    }
}
