using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NinaSpeakV2.Data.Entities;

namespace NinaSpeakV2.Data.EntitiesMapping
{
    public class ChatBotGenreEntityMapping : BaseEntityEnumMapping<ChatBotGenre>
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
