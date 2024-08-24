using System.Text.Json.Serialization;

namespace NinaSpeakV2.Domain.ViewModels.UsersInstitutions
{
    public class CreateUserInstitutionViewModel : BaseCreateViewModel
    {
        [JsonIgnore]
        public long UserFk { get; set; }

        [JsonInclude]
        public string UserEmail { get; set; }

        [JsonInclude]
        public string UserPassword { get; set; }

        [JsonInclude]
        public string UserConfirmPassword { get; set; }

        [JsonInclude]
        public long InstitutionFk { get; set; }

        [JsonIgnore]
        public string InstitutionCode { get; set; }

        [JsonIgnore]
        public bool Owner { get; set; }
        
        [JsonIgnore]
        public bool Creator { get; set; }
    }
}
