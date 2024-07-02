namespace NinaSpeakV2.Data.Models
{
    public class User : BaseModel
    {
        public User()
        {
            UserInstitutions = new List<UserInstitution>();
        }

        public string Email { get; set; }

        public string Password { get; set; }

        public string Salt { get; set; }

        //Activated: bool

        public ICollection<UserInstitution> UserInstitutions { get; set; }
    }
}
