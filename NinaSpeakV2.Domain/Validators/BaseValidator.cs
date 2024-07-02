namespace NinaSpeakV2.Domain.Validators
{
    public abstract class BaseValidator
    {
        public const long IdMinValue = 1;

        public static bool IsValid<T>(T? value)
        {
            return value is not null;
        }
        
        public static bool IsValid<T>(IEnumerable<T>? values)
        {
            return values is not null && values.Any();
        }
        
        public static bool IsValid<T>(params T[]? values)
        {
            return values is not null;
        }

        public static bool IsValid(string? value)
        {
            return !string.IsNullOrEmpty(value) && !string.IsNullOrWhiteSpace(value);
        }

        public static bool IsBetween(long value, long min, long max)
        {
            return value >= min && value <= max;
        }

        public static bool IsBetween(decimal value, decimal min, decimal max)
        {
            return value >= min && value <= max;
        }

        public static bool IsAbove(long value, long min)
        {
            return value >= min;
        }

        public static bool IsAbove(decimal value, decimal min)
        {
            return value >= min;
        }

        public static bool IsBellow(long value, long max)
        {
            return value <= max;
        }

        public static bool IsBellow(decimal value, decimal max)
        {
            return value <= max;
        }
    }
}
