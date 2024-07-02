using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NinaSpeakV2.Data.Models;

namespace NinaSpeakV2.Data.ModelsMapping
{
    public class UserModelMapping : BaseModelMapping<User>
    {
        public override void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("Usuario");
            base.Configure(builder);

            builder
                .Property(m => m.Email)
                .HasMaxLength(256)
                .IsRequired()
                .HasColumnName("Email");

            builder
                .Property(m => m.Password)
                .HasMaxLength(128)
                .IsRequired()
                .HasColumnName("Senha");
            
            builder
                .Property(m => m.Salt)
                .HasMaxLength(36)
                .IsRequired()
                .HasColumnName("Sal");            
        }
    }
}
