using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NinaSpeakV2.Data.Models;

namespace NinaSpeakV2.Data.ModelsMapping
{
    public class ChatBotModelMapping : BaseModelEnumMapping<ChatBot>
    {
        public override void Configure(EntityTypeBuilder<ChatBot> builder)
        {
            builder.ToTable("ChatBot");
            base.Configure(builder);

            builder
                .Property(m => m.ChatBotGenreFk)
                .IsRequired()
                .HasColumnName("ChatBotGeneroFk");
            builder
                .HasOne(m => m.ChatBotGenre)
                .WithMany(m => m.ChatBots)
                .HasForeignKey(m => m.ChatBotGenreFk)
                .OnDelete(DeleteBehavior.Restrict);

            builder
                .Property(m => m.InstitutionFk)
                .IsRequired()
                .HasColumnName("InstituicaoFk");
            builder
                .HasOne(m => m.Institution)
                .WithMany(m => m.ChatBots)
                .HasForeignKey(m => m.InstitutionFk)
                .OnDelete(DeleteBehavior.Restrict);

            builder
                .Property(m => m.Name)
                .HasMaxLength(80)
                .IsRequired()
                .HasColumnName("Nome");                
        }
    }
}
