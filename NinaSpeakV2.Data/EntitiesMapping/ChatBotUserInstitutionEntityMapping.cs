using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NinaSpeakV2.Data.Entities;

namespace NinaSpeakV2.Data.EntitiesMapping
{
    public class ChatBotUserInstitutionEntityMapping : BaseEntityMapping<ChatBotUserInstitution>
    {
        public override void Configure(EntityTypeBuilder<ChatBotUserInstitution> builder)
        {
            builder.ToTable("ChatBotUsuarioInstituicao");
            base.Configure(builder);

            builder
                .Property(m => m.ChatBotFk)
                .IsRequired()
                .HasColumnName("ChatBotFk");
            builder
                .HasOne(m => m.ChatBot)
                .WithMany(m => m.ChatBotUserInstitutions)
                .HasForeignKey(m => m.ChatBotFk)
                .OnDelete(DeleteBehavior.Restrict);

            builder
                .Property(m => m.UserInstitutionFk)
                .IsRequired()
                .HasColumnName("UsuarioInstituicaoFk");
            builder
                .HasOne(m => m.UserInstitution)
                .WithMany(m => m.ChatBotUserInstitutions)
                .HasForeignKey(m => m.UserInstitutionFk)
                .OnDelete(DeleteBehavior.Restrict);

            builder
                .Property(m => m.Writer)
                .IsRequired()
                .HasColumnName("Escritor");

            builder
                .Property(m => m.Reader)
                .IsRequired()
                .HasColumnName("Leitor");            
        }
    }
}
