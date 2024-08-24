using System.Text.Json.Serialization;

namespace NinaSpeakV2.Domain.ViewModels.Users
{
    public class UpdateUserPasswordViewModel : UpdateUserViewModel
    {
        [JsonInclude]
        public string Password { get; set; }

        [JsonInclude]
        public string NewPassword { get; set; }

        [JsonInclude]
        public string ConfirmNewPassword { get; set; }
    }
}
