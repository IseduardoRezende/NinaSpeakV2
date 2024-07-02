using System.Text.Json.Serialization;

namespace NinaSpeakV2.Domain.ViewModels.ChatBots
{
    public class UpdateChatBotViewModel : BaseUpdateEnumViewModel
    {
        [JsonInclude]
        public string Name { get; set; }

        [JsonInclude]
        public long ChatBotGenreFk { get; set; }
    }
}
