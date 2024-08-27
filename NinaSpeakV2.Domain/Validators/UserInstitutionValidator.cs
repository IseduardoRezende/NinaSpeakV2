using NinaSpeakV2.Data.Entities;
using NinaSpeakV2.Domain.ViewModels.UsersInstitutions;

namespace NinaSpeakV2.Domain.Validators
{
    public class UserInstitutionValidator : BaseValidator
    {
        public static bool IsEqual(UserInstitution userInstitution, UpdateUserInstitutionViewModel updateModel)
        {
            ArgumentNullException.ThrowIfNull(userInstitution, nameof(userInstitution));
            ArgumentNullException.ThrowIfNull(updateModel, nameof(updateModel));

            return userInstitution.Owner == updateModel.Owner;
        }

        public static bool ContainsLoad(UserInstitution userInstitution)
        {
            ArgumentNullException.ThrowIfNull(userInstitution, nameof(userInstitution));
            return userInstitution.Creator || userInstitution.Owner;
        }
    }
}
