using System.Text.Json.Serialization;

namespace NinaSpeakV2.Domain.ViewModels.Users
{
    public class UpdateUserViewModel : BaseUpdateViewModel
    {
        [JsonInclude]
        public string Email { get; set; }

        [JsonInclude]
        public string NewEmail { get; set; }

        [JsonInclude]
        public string ConfirmNewEmail { get; set; }       
    }
}
