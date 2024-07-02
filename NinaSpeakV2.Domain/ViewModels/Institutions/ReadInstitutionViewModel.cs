using System.Text.Json.Serialization;

namespace NinaSpeakV2.Domain.ViewModels.Institutions
{
    public class ReadInstitutionViewModel : BaseReadEnumViewModel
    {
        [JsonInclude]
        public string Name { get; set; }

        [JsonInclude]
        public string? Image { get; set; }
    }
}
