namespace NinaSpeakV2.Data.Entities
{
    public class ChatBotUserInstitution : BaseEntity
    {
        public long ChatBotFk { get; set; }

        public long UserInstitutionFk { get; set; }

        public bool Writer { get; set; }

        public bool Reader { get; set; }

        public ChatBot ChatBot { get; set; }
        
        public UserInstitution UserInstitution { get; set; }
    }
}
