using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NinaSpeakV2.Data.Models;

namespace NinaSpeakV2.Data.ModelsMapping
{
    public class ChatBotConversationModelMapping : BaseModelMapping<ChatBotConversation>
    {
        public override void Configure(EntityTypeBuilder<ChatBotConversation> builder)
        {
            builder.ToTable("ChatBotConversa");
            base.Configure(builder);

            builder
                .Property(m => m.ChatBotFk)
                .IsRequired()
                .HasColumnName("ChatBotFk");
            builder
                .HasOne(m => m.ChatBot)
                .WithMany(m => m.ChatBotConversations)
                .HasForeignKey(m => m.ChatBotFk)
                .OnDelete(DeleteBehavior.Restrict);

            builder
                .Property(m => m.Message)
                .HasMaxLength(150)
                .IsRequired()
                .HasColumnName("Mensagem");

            builder
                .Property(m => m.Response)
                .HasMaxLength(150)
                .IsRequired()
                .HasColumnName("Resposta");
        }
    }
}
