namespace NinaSpeakV2.Domain.Models
{
    public class BaseError
    {
        public const string NullObject = "Object Value is null.";
        public const string InternalProcessError = "An Internal Processing Error has Occurred.";
        public const string NoChangesDetected = "No changes was detected.";
        public const string InexistentObject = "Inexistent Object found.";
        public const string InvalidFilters = "Invalid Filters.";
        public const string InvalidIncludes = "Invalid Includes.";
        public const string InvalidValue = "Invalid Value.";
        public const string InvalidValues = "Invalid Values.";

        public const string NameAlreadyExist = "Name already exist.";

        public const string UserNotFound = "User not found.";
        public const string UserNotAuthenticated = "User not authenticated.";
        public const string UserInstitutionNotFound = "User Institution not found.";
        public const string UserInstitutionAlreadyExist = "Member already exists in the Institution.";
        public const string UserInstitutionLoad = "Member don't have an load.";

        public const string InstitutionNotFound = "Institution not found.";
        
        public const string ChatBotNotFound = "ChatBot not found.";
        public const string ChatBotConversationNotFound = "ChatBot Conversation not found.";
        public const string ChatBotGenreNotFound = "ChatBot Genre not found.";
        public const string ChatBotTypeNotFound = "ChatBot Type not found.";
        
        public const string InvalidPassword = "Invalid Password.";
        public const string PasswordNotMatch = "Password and Confirm Password don't match.";
        
        public const string InvalidDescription = "Invalid Description.";
        public const string InvalidEmail = "Invalid Email.";
        public const string InvalidCode = "Invalid Institution Code.";
        public const string InvalidName = "Invalid Name.";
        public const string InvalidImage = "Invalid Image.";        
        public const string InvalidMessage = "Invalid Message.";
        public const string InvalidResponse = "Invalid Response.";
        
        public const string EmailAlreadyExist = "Email already exist.";
        public const string EmailNotMatch = "New Email and Confirm New Email don't match.";

        public const string UserInstitutionsErrorSpan = "An Institution is required to Create a ChatBot";        
        public const string ChatBotGenresErrorSpan = "An ChatBot Genre is required to Create a ChatBot";
        public const string ChatBotTypesErrorSpan = "An ChatBot Type is required to Create a ChatBot";
        public const string ChatBotsErrorSpan = "An ChatBot is required to Create a Conversation";

        public BaseError(string description)
        {
            ArgumentNullException.ThrowIfNull(description, nameof(description));
            Description = description;
        }

        public string Description { get; set; }
    }
}
