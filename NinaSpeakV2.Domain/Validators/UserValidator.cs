using NinaSpeakV2.Data.Entities;
using System.ComponentModel.DataAnnotations;

namespace NinaSpeakV2.Domain.Validators
{
    public class UserValidator : BaseValidator
    {
        public const int PasswordMaxLength = 20;
        public const int PasswordMinLength = 8;

        public static bool IsAuthenticated(User user)
        {
            ArgumentNullException.ThrowIfNull(user, nameof(user));
            return user.Authenticated;
        }

        public static bool IsValidPassword(string password)
        {
            return IsValid(password) && IsBetween(password.Length, PasswordMinLength, PasswordMaxLength);
        }

        public static bool IsValidEmail(string email)
        {
            return new EmailAddressAttribute().IsValid(email);
        }
    }
}
