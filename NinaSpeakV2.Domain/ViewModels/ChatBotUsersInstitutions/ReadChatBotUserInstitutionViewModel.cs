using System.Text.Json.Serialization;

namespace NinaSpeakV2.Domain.ViewModels.ChatBotUsersInstitutions
{
    public class ReadChatBotUserInstitutionViewModel : BaseReadViewModel
    {
        [JsonInclude]
        public long ChatBotFk { get; set; }

        [JsonInclude]
        public long UserInstitutionFk { get; set; }

        [JsonInclude]
        public string ChatBotName { get; set; }
        
        [JsonInclude]
        public string UserInstitutionInstitutionName { get; set; }

        [JsonInclude]
        public long UserInstitutionUserId { get; set; }

        [JsonInclude]
        public string UserInstitutionUserEmail { get; set; }

        [JsonInclude]
        public bool UserInstitutionOwner { get; set; }

        [JsonInclude]
        public bool UserInstitutionCreator { get; set; }

        [JsonInclude]
        public bool Writer { get; set; }

        [JsonInclude]
        public bool Reader { get; set; }
    }
}
