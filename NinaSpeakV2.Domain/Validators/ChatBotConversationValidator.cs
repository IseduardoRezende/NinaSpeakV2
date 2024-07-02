using NinaSpeakV2.Data.Models;
using NinaSpeakV2.Domain.ViewModels.ChatBotConversations;

namespace NinaSpeakV2.Domain.Validators
{
    public class ChatBotConversationValidator : BaseValidator
    {
        public const int MessageMinLength = 1;
        public const int MessageMaxLength = 150;

        public const int ResponseMinLength = 1;
        public const int ResponseMaxLength = 150;

        public static bool IsEqual(ChatBotConversation chatBotConversation, UpdateChatBotConversationViewModel updateModel)
        {
            ArgumentNullException.ThrowIfNull(chatBotConversation, nameof(chatBotConversation));
            ArgumentNullException.ThrowIfNull(updateModel, nameof(updateModel));

            return chatBotConversation.Message == updateModel.Message &&
                   chatBotConversation.Response == updateModel.Response;
        }

        public static bool IsValidMessage(string message)
        {
            return IsValid(message) && IsBetween(message.Length, MessageMinLength, ResponseMaxLength);
        }

        public static bool IsValidResponse(string response)
        {
            return IsValid(response) && IsBetween(response.Length, ResponseMinLength, ResponseMaxLength);
        }
    }
}
