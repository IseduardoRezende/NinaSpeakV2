using System.Text.Json.Serialization;

namespace NinaSpeakV2.Domain.ViewModels.ChatBotConversations
{
    public class ReadChatBotConversationViewModel : BaseReadViewModel
    {
        [JsonInclude]
        public long ChatBotFk { get; set; }
        
        [JsonInclude]
        public string ChatBotName { get; set; }

        [JsonInclude]
        public string Message { get; set; }

        [JsonInclude]
        public string Response { get; set; }
    }
}
