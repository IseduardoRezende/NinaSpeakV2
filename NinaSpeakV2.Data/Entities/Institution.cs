namespace NinaSpeakV2.Data.Entities
{
    public class Institution : BaseEntityEnum
    {
        public const string StandardName = "ChatBot de Graça";

        public Institution()
        {
            ChatBots = new List<ChatBot>();
            UserInstitutions = new List<UserInstitution>();
        }

        public string Name { get; set; }

        public string Code { get; set; }

        public string? Image { get; set; }

        public ICollection<ChatBot> ChatBots { get; set; }

        public ICollection<UserInstitution> UserInstitutions { get; set; }
    }
}
