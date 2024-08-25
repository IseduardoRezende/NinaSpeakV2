namespace NinaSpeakV2.Data.Entities
{
    public class ChatBot : BaseEntityEnum
    {
        public ChatBot()
        {
            ChatBotConversations = new List<ChatBotConversation>();
            ChatBotUserInstitutions = new List<ChatBotUserInstitution>();
        }

        public string Name { get; set; }

        public long ChatBotGenreFk { get; set; }

        public long ChatBotTypeFk { get; set; }

        public long InstitutionFk { get; set; }

        public ChatBotGenre ChatBotGenre { get; set; }

        public ChatBotType ChatBotType { get; set; }

        public Institution Institution { get; set; }

        public ICollection<ChatBotConversation> ChatBotConversations { get; set; }

        public ICollection<ChatBotUserInstitution> ChatBotUserInstitutions { get; set; }
    }
}
