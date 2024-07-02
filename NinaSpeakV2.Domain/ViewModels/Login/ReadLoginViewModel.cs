using NinaSpeakV2.Domain.ViewModels.Users;
using System.Text.Json.Serialization;

namespace NinaSpeakV2.Domain.ViewModels.Login
{
    public class ReadLoginViewModel : ReadUserViewModel
    {        
        [JsonInclude]
        public string Password { get; set; }
    }
}
