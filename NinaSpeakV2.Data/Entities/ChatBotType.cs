namespace NinaSpeakV2.Data.Entities
{
    public class ChatBotType : BaseEntityEnum
    {
        public ChatBotType()
        {
            ChatBots = new List<ChatBot>();
        }
        
        public ICollection<ChatBot> ChatBots { get; set; }
    }
}
