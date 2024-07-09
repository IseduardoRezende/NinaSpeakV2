using System.Text.Json.Serialization;

namespace NinaSpeakV2.Domain.ViewModels.UsersInstitutions
{
    public class UpdateUserInstitutionViewModel : BaseUpdateViewModel
    {
        [JsonIgnore]
        public long UserFk { get; set; }

        [JsonInclude]
        public string UserEmail { get; set; }

        [JsonIgnore]
        public long InstitutionFk { get; set; }

        [JsonInclude]
        public string InstitutionName { get; set; }

        [JsonInclude]
        public bool Owner { get; set; }

        [JsonInclude]
        public bool Writer { get; set; }
    }
}
