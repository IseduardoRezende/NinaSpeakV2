namespace NinaSpeakV2.Domain.Validators
{
    public abstract class BaseEnumValidator : BaseValidator
    {
        public const int DescriptionMaxLength = 150;
        public const int DescriptionMinLength = 5;

        public static bool IsValidDescription(string? description)
        {
            return description is null || 
                 (IsValid(description) && IsBetween(description.Length, DescriptionMinLength, DescriptionMaxLength));
        }
    }
}
