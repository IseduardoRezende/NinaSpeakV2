using System.Text.Json.Serialization;

namespace NinaSpeakV2.Domain.ViewModels.ChatBots
{
    public class ReadChatBotViewModel : BaseReadEnumViewModel
    {
        [JsonInclude]
        public string Name { get; set; }

        [JsonInclude]
        public long ChatBotGenreFk { get; set; }

        [JsonInclude]
        public long ChatBotTypeFk { get; set; }

        [JsonInclude]
        public long InstitutionFk { get; set; }

        [JsonInclude]
        public string ChatBotGenreDescription { get; set; }

        [JsonInclude]
        public string ChatBotTypeDescription { get; set; }

        [JsonInclude]
        public string InstitutionName { get; set; }
    }
}
