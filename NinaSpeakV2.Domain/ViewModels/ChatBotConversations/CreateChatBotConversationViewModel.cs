using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using NinaSpeakV2.Domain.Models;

namespace NinaSpeakV2.Domain.ViewModels.ChatBotConversations
{
    public class CreateChatBotConversationViewModel : BaseCreateViewModel
    {
        [Required(ErrorMessage = BaseError.ChatBotsErrorSpan), JsonInclude]
        public long ChatBotFk { get; set; }

        [JsonInclude]
        public string Message { get; set; }

        [JsonInclude]
        public string Response { get; set; }
    }
}
