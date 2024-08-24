using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NinaSpeakV2.Data.Entities;

namespace NinaSpeakV2.Data.EntitiesMapping
{
    public class UserInstitutionEntityMapping : BaseEntityGlobalMapping<UserInstitution>
    {
        public override void Configure(EntityTypeBuilder<UserInstitution> builder)
        {
            builder.ToTable("UsuarioInstituicao");
            base.Configure(builder);

            builder.HasKey(m => new { m.UserFk, m.InstitutionFk });

            builder
                .Property(m => m.UserFk)
                .IsRequired()
                .HasColumnName("UsuarioFk");
            builder
                .HasOne(m => m.User)
                .WithMany(m => m.UserInstitutions)
                .HasForeignKey(m => m.UserFk)
                .OnDelete(DeleteBehavior.Restrict);

            builder
                .Property(m => m.InstitutionFk)
                .IsRequired()
                .HasColumnName("InstituicaoFk");
            builder
                .HasOne(m => m.Institution)
                .WithMany(m => m.UserInstitutions)
                .HasForeignKey(m => m.InstitutionFk)
                .OnDelete(DeleteBehavior.Restrict);

            builder
                .Property(m => m.Owner)
                .HasDefaultValue(false)
                .IsRequired()
                .HasColumnName("Proprietario");
            
            builder
               .Property(m => m.Creator)
               .HasDefaultValue(false)
               .IsRequired()
               .HasColumnName("Criador");
        }
    }
}
