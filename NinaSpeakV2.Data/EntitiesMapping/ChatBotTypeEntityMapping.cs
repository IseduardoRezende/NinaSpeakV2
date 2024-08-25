using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NinaSpeakV2.Data.Entities;

namespace NinaSpeakV2.Data.EntitiesMapping
{
    public class ChatBotTypeEntityMapping : BaseEntityEnumMapping<ChatBotType>
    {
        public override void Configure(EntityTypeBuilder<ChatBotType> builder)
        {
            builder.ToTable("ChatBotTipo");
            base.Configure(builder);

            builder
                .HasIndex(m => m.Description)
                .IsUnique();
        }
    }
}
