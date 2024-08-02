namespace NinaSpeakV2.Data.Interfaces
{
    public interface IBaseEntityGlobal
    {
        public DateTime CreatedAt { get; set; }

        public DateTime? DeletedAt { get; set; }
    }
}
