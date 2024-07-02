namespace NinaSpeakV2.Api.Utils
{
    public class Constant
    {
        public const string ViewDataBaseErrors               = "BaseErrors";
        public const string ViewDataUserId                   = "UserId";
        public const string ViewDataUserEmail                = "UserEmail";        
        public const string ViewDataChatBotGenres            = "ChatBotGenres";
        public const string ViewDataUserInstitutions         = "UserInstitutions";       
        public const string ViewDataChatBots                 = "ChatBots";
        public const string ViewDataChatBotId                = "ChatBotId";

        public const string UserInstitutionsErrorSpan        = "An Institution is required to Create a ChatBot"; //Show this message for input error on Views
        public const string ChatBotsErrorSpan                = "A ChatBot is required to Create an Conversation"; //Show this message for input error on Views

        public const string PartialViewNameBaseErrors        = "_BaseErrors";        
    }
}
