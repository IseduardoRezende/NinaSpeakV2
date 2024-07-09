using Microsoft.AspNetCore.Http;
using System.Text.Json.Serialization;

namespace NinaSpeakV2.Domain.ViewModels.Institutions
{
    public class UpdateInstitutionViewModel : BaseUpdateEnumViewModel
    {
        [JsonInclude]
        public string Name { get; set; }

        [JsonInclude]
        public IFormFile? Image { get; set; }

        [JsonIgnore]
        public string? FileName { get { return Image?.FileName; } }
    }
}
