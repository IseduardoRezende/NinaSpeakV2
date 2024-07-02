using Microsoft.AspNetCore.Http;
using System.Text.Json.Serialization;

namespace NinaSpeakV2.Domain.ViewModels.Institutions
{
    public class CreateInstitutionViewModel : BaseCreateEnumViewModel
    {
        [JsonInclude]
        public string Name { get; set; }

        [JsonInclude]
        public IFormFile? Image { get; set; }

        [JsonIgnore]
        public string? FileName { get { return Image?.FileName; } }

        [JsonIgnore]
        public long UserFk { get; set; }
    }
}
