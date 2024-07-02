namespace NinaSpeakV2.Data.Models
{
    public class ChatBotConversation : BaseModel
    {
        public long ChatBotFk { get; set; }

        public string Message { get; set; }

        public string Response { get; set; }

        public ChatBot ChatBot { get; set; }
    }
}
