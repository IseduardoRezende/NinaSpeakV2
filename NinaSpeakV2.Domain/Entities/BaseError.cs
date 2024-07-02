namespace NinaSpeakV2.Domain.Entities
{
    public class BaseError
    {
        public const string InternalProcessError              = "An Internal Processing Error has Occurred.";
        public const string NoChangesDetected                 = "No changes was detected.";
        public const string InexistentObject                  = "Inexistent Object found.";
        public const string NullObject                        = "Object Value is null.";
        public const string UserNotFound                      = "User not found.";
        public const string InstitutionNotFound               = "Institution not found.";
        public const string ChatBotNotFound                   = "ChatBot not found.";
        public const string ChatBotConversationNotFound       = "ChatBot Conversation not found.";
        public const string ChatBotGenreNotFound              = "ChatBot Genre not found.";
        public const string PasswordNotMatch                  = "Password and Confirm Password don't match.";
        public const string InvalidFilters                    = "Invalid Filters.";
        public const string InvalidIncludes                   = "Invalid Includes.";
        public const string InvalidValue                      = "Invalid Value.";
        public const string InvalidPassword                   = "Invalid Password.";
        public const string InvalidDescription                = "Invalid Description.";
        public const string InvalidEmail                      = "Invalid Email.";
        public const string InvalidName                       = "Invalid Name.";
        public const string InvalidMessage                    = "Invalid Message.";
        public const string InvalidResponse                   = "Invalid Response.";
        public const string InvalidImage                      = "Invalid Image.";
        public const string EmailAlreadyExist                 = "Email already exist.";
        public const string NameAlreadyExist                  = "Name already exist.";

        public BaseError(string description)
        {
            ArgumentNullException.ThrowIfNull(description, nameof(description));
            Description = description;
        }

        public string Description { get; set; }
    }
}
