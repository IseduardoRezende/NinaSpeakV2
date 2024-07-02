namespace NinaSpeakV2.Data.Models
{
    public class ChatBotGenre : BaseModelEnum
    {
        public ChatBotGenre()
        {
            ChatBots = new List<ChatBot>();
        }

        public ICollection<ChatBot> ChatBots { get; set; }
    }
}
