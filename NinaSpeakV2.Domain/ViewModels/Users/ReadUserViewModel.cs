using System.Text.Json.Serialization;

namespace NinaSpeakV2.Domain.ViewModels.Users
{
    public class ReadUserViewModel : BaseReadViewModel
    {
        [JsonInclude]
        public string Email { get; set; }        
    }
}
