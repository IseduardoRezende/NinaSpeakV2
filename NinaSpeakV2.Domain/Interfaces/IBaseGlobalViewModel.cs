namespace NinaSpeakV2.Domain.Interfaces
{
    public interface IBaseGlobalViewModel : IBaseErrorViewModel
    {
        public DateTime CreatedAt { get; set; }

        public DateTime? DeletedAt { get; set; }
    }
}
