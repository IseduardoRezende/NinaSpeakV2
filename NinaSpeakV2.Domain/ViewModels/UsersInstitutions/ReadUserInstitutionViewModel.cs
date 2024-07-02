using System.Text.Json.Serialization;

namespace NinaSpeakV2.Domain.ViewModels.UsersInstitutions
{
    public class ReadUserInstitutionViewModel : BaseReadViewModel
    {
        [JsonInclude]
        public long UserFk { get; set; }

        [JsonInclude]
        public long InstitutionFk { get; set; }

        [JsonInclude]
        public string UserEmail { get; set; }

        [JsonInclude]
        public string InstitutionName { get; set; }

        [JsonInclude]
        public bool Owner { get; set; }

        [JsonInclude]
        public bool Writer { get; set; }
    }
}
