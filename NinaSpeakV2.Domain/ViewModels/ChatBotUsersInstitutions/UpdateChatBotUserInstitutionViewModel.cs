namespace NinaSpeakV2.Domain.ViewModels.ChatBotUsersInstitutions
{
    public class UpdateChatBotUserInstitutionViewModel : BaseUpdateViewModel
    {
        public long ChatBotFk { get; set; }

        public long UserInstitutionFk { get; set; }

        public bool Writer { get; set; }

        public bool Reader { get; set; }
    }
}
