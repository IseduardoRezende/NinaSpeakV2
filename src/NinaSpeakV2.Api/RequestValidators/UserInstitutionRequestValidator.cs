using NinaSpeakV2.Domain.Validators;
using NinaSpeakV2.Domain.ViewModels.UsersInstitutions;

namespace NinaSpeakV2.Api.RequestValidators
{
    public class UserInstitutionRequestValidator
    {
        public static bool IsCreator(string userEmail, IEnumerable<ReadUserInstitutionViewModel> members)
        {
            ArgumentNullException.ThrowIfNull(userEmail, nameof(userEmail));
            ArgumentNullException.ThrowIfNull(members, nameof(members));

            var creator = members.FirstOrDefault(m => m.Creator);
            return userEmail == (creator?.UserEmail ?? string.Empty);
        }

        public static bool IsOwner(string userEmail, IEnumerable<ReadUserInstitutionViewModel> members)
        {
            ArgumentNullException.ThrowIfNull(userEmail, nameof(userEmail));
            ArgumentNullException.ThrowIfNull(members, nameof(members));

            return members.Any(m => m.UserEmail == userEmail && m.Owner);
        }

        public static bool IsMember(string userEmail, IEnumerable<ReadUserInstitutionViewModel> members)
        {
            ArgumentNullException.ThrowIfNull(userEmail, nameof(userEmail));
            ArgumentNullException.ThrowIfNull(members, nameof(members));

            return members.Any(m => m.UserEmail == userEmail);
        }

        public static bool IsCreator(long? userFk, IEnumerable<ReadUserInstitutionViewModel> members)
        {
            ArgumentNullException.ThrowIfNull(members, nameof(members));
            
            if (!BaseValidator.IsValid(userFk) || !BaseValidator.IsAbove(userFk!.Value, BaseValidator.IdMinValue))
                return false;

            return members.Any(m => m.UserFk == userFk && m.Creator);
        }

        public static bool IsOwner(long? userFk, IEnumerable<ReadUserInstitutionViewModel> members)
        {
            ArgumentNullException.ThrowIfNull(members, nameof(members));

            if (!BaseValidator.IsValid(userFk) || !BaseValidator.IsAbove(userFk!.Value, BaseValidator.IdMinValue))
                return false;

            return members.Any(m => m.UserFk == userFk && m.Owner);
        }

        public static bool IsMember(long? userFk, IEnumerable<ReadUserInstitutionViewModel> members)
        {
            ArgumentNullException.ThrowIfNull(members, nameof(members));

            if (!BaseValidator.IsValid(userFk) || !BaseValidator.IsAbove(userFk!.Value, BaseValidator.IdMinValue))
                return false;

            return members.Any(m => m.UserFk == userFk);
        }

        public static bool IsCreator(ReadUserInstitutionViewModel member)
        {
            ArgumentNullException.ThrowIfNull(member, nameof(member));
            return member.Creator;
        }

        public static bool IsOwner(ReadUserInstitutionViewModel member)
        {
            ArgumentNullException.ThrowIfNull(member, nameof(member));
            return member.Owner;
        }

        public static bool IsMember(ReadUserInstitutionViewModel member)
        {
            return BaseValidator.IsValid(member);
        }
    }
}
