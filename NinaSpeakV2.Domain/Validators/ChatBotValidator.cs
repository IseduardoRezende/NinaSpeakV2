﻿using NinaSpeakV2.Data.Entities;
using NinaSpeakV2.Domain.ViewModels.ChatBots;

namespace NinaSpeakV2.Domain.Validators
{
    public class ChatBotValidator : BaseEnumValidator
    {
        public const int NameMaxLength = 80;
        public const int NameMinLength = 1;

        public const string StandardName = "Nosso ChatBot";

        public static bool IsValidName(string name)
        {
            return IsValid(name) && IsBetween(name.Length, NameMinLength, NameMaxLength) && name is not StandardName;
        }

        public static bool IsEqual(ChatBot chatBot, UpdateChatBotViewModel updateModel)
        {
            ArgumentNullException.ThrowIfNull(chatBot, nameof(chatBot));
            ArgumentNullException.ThrowIfNull(updateModel, nameof(updateModel));

            return chatBot.Name == updateModel.Name &&
                   chatBot.Description == updateModel.Description &&
                   chatBot.ChatBotGenreFk == updateModel.ChatBotGenreFk;
        }
    }
}
