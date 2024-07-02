namespace NinaSpeakV2.Data.Models
{
    public class Institution : BaseModelEnum
    {
        public const string StandardName = "ChatBot de Graça";

        public Institution()
        {
            ChatBots = new List<ChatBot>();
            UserInstitutions = new List<UserInstitution>();
        }

        public string Name { get; set; }

        public string? Image { get; set; }

        public ICollection<ChatBot> ChatBots { get; set; }

        public ICollection<UserInstitution> UserInstitutions { get; set; }
    }
}
