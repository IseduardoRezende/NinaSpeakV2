namespace NinaSpeakV2.Data.Entities
{
    public class ChatBotGenre : BaseEntityEnum
    {
        public ChatBotGenre()
        {
            ChatBots = new List<ChatBot>();
        }

        public ICollection<ChatBot> ChatBots { get; set; }
    }
}
