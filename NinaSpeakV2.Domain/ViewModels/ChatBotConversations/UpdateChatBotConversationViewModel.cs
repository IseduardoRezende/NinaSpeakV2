using System.Text.Json.Serialization;

namespace NinaSpeakV2.Domain.ViewModels.ChatBotConversations
{
    public class UpdateChatBotConversationViewModel : BaseUpdateViewModel
    {        
        [JsonInclude]
        public string Message { get; set; }

        [JsonInclude]
        public string Response { get; set; }
    }
}
