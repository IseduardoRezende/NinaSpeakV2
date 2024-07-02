using System.Text.Json.Serialization;

namespace NinaSpeakV2.Domain.ViewModels.ChatBots
{
    public class CreateChatBotViewModel : BaseCreateEnumViewModel
    {
        [JsonInclude]
        public string Name { get; set; }

        [JsonInclude]
        public long ChatBotGenreFk { get; set; }

        [JsonInclude]
        public long InstitutionFk { get; set; }
    }
}
