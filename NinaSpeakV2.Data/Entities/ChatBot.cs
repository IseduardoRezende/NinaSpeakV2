namespace NinaSpeakV2.Data.Entities
{
    public class ChatBot : BaseEntityEnum
    {
        public ChatBot()
        {
            ChatBotConversations = new List<ChatBotConversation>();
        }

        public string Name { get; set; }

        public long ChatBotGenreFk { get; set; }

        public long InstitutionFk { get; set; }

        public ChatBotGenre ChatBotGenre { get; set; }

        public Institution Institution { get; set; }

        public ICollection<ChatBotConversation> ChatBotConversations { get; set; }
    }
}
