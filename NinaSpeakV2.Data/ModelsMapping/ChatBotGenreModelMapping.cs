using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NinaSpeakV2.Data.Models;

namespace NinaSpeakV2.Data.ModelsMapping
{
    public class ChatBotGenreModelMapping : BaseModelEnumMapping<ChatBotGenre>
    {
        public override void Configure(EntityTypeBuilder<ChatBotGenre> builder)
        {
            builder.ToTable("ChatBotGenero");
            base.Configure(builder);

            builder
                .HasIndex(m => m.Description)
                .IsUnique();                   
        }
    }
}
