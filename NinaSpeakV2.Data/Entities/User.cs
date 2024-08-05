namespace NinaSpeakV2.Data.Entities
{
    public class User : BaseEntity
    {
        public User()
        {
            UserInstitutions = new List<UserInstitution>();
        }

        public string Email { get; set; }

        public string Password { get; set; }

        public string Salt { get; set; }

        public bool Authenticated { get; set; }

        public short? VerificationCode { get; set; }

        public ICollection<UserInstitution> UserInstitutions { get; set; }
    }
}
