using NinaSpeakV2.Domain.ViewModels.Users;
using System.Text.Json.Serialization;

namespace NinaSpeakV2.Domain.ViewModels.Institutions
{
    public class ReadInstitutionViewModel : BaseReadEnumViewModel
    {
        [JsonInclude]
        public string Name { get; set; }

        [JsonInclude]
        public string? Image { get; set; }

        [JsonInclude]
        public long QtyMembers { get { return Members.LongCount(); } }

        [JsonInclude]
        public IEnumerable<ReadUserViewModel> Members { get; set; } = Enumerable.Empty<ReadUserViewModel>();
    }
}
