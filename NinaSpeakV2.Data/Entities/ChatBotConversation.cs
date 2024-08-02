namespace NinaSpeakV2.Data.Entities
{
    public class ChatBotConversation : BaseEntity
    {
        public long ChatBotFk { get; set; }

        public string Message { get; set; }

        public string Response { get; set; }

        public ChatBot ChatBot { get; set; }
    }
}
