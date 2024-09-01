using NinaSpeakV2.Domain.Validators;
using NinaSpeakV2.Domain.ViewModels.ChatBotUsersInstitutions;

namespace NinaSpeakV2.Api.RequestValidators
{
    public class ChatBotUserInstitutionRequestValidator
    {
        public static bool IsMember(string userEmail, IEnumerable<ReadChatBotUserInstitutionViewModel> members)
        {
            ArgumentNullException.ThrowIfNull(userEmail, nameof(userEmail));
            ArgumentNullException.ThrowIfNull(members, nameof(members));

            return members.Any(m => (m.UserInstitutionUserEmail ?? string.Empty) == userEmail);
        }

        public static bool IsMember(long? userFk, IEnumerable<ReadChatBotUserInstitutionViewModel> members)
        {
            ArgumentNullException.ThrowIfNull(members, nameof(members));

            if (!BaseValidator.IsValid(userFk) || !BaseValidator.IsAbove(userFk!.Value, BaseValidator.IdMinValue))
                return false;

            return members.Any(m => m.UserInstitutionUserId == userFk);
        }
    }
}
