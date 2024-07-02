using System.Text.Json.Serialization;

namespace NinaSpeakV2.Domain.ViewModels.Users
{
    public class UpdateUserViewModel : BaseUpdateViewModel
    {
        [JsonInclude]
        public string Email { get; set; }

        [JsonInclude]
        public string Password { get; set; }

        [JsonInclude]
        public string ConfirmPassword { get; set; }

        [JsonIgnore]
        public long UserFk { get; set; }
    }
}
