using NinaSpeakV2.Domain.ViewModels.UsersInstitutions;
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
        public IEnumerable<ReadUserInstitutionViewModel> Members { get; set; } = Enumerable.Empty<ReadUserInstitutionViewModel>();

        [JsonInclude]
        public ReadUserInstitutionViewModel? Creator { get { return Members.FirstOrDefault(ui => ui.Creator); } }
    }
}
