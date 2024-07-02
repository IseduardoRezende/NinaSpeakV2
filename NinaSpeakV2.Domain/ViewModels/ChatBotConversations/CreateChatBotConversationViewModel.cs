using System.Text.Json.Serialization;

namespace NinaSpeakV2.Domain.ViewModels.ChatBotConversations
{
    public class CreateChatBotConversationViewModel : BaseCreateViewModel
    {
        [JsonInclude]
        public long ChatBotFk { get; set; }

        [JsonInclude]
        public string Message { get; set; }

        [JsonInclude]
        public string Response { get; set; }
    }
}
