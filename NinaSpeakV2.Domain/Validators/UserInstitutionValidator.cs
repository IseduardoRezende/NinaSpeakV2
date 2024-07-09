using NinaSpeakV2.Data.Models;
using NinaSpeakV2.Domain.ViewModels.UsersInstitutions;

namespace NinaSpeakV2.Domain.Validators
{
    public class UserInstitutionValidator : BaseValidator
    {
        public static bool IsEqual(UserInstitution userInstitution, UpdateUserInstitutionViewModel updateModel)
        {
            ArgumentNullException.ThrowIfNull(userInstitution, nameof(userInstitution));
            ArgumentNullException.ThrowIfNull(updateModel, nameof(updateModel));

            return userInstitution.Owner == updateModel.Owner &&
                   userInstitution.Writer == updateModel.Writer;
        }
    }
}
