﻿using NinaSpeakV2.Domain.Entities;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace NinaSpeakV2.Domain.ViewModels.ChatBots
{
    public class UpdateChatBotViewModel : BaseUpdateEnumViewModel
    {
        [JsonInclude]
        public string Name { get; set; }

        [Required(ErrorMessage = BaseError.ChatBotGenresErrorSpan), JsonInclude]
        public long ChatBotGenreFk { get; set; }
    }
}
