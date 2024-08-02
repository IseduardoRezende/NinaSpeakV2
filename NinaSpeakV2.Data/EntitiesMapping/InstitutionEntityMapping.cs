using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NinaSpeakV2.Data.Entities;

namespace NinaSpeakV2.Data.EntitiesMapping
{
    public class InstitutionEntityMapping : BaseEntityEnumMapping<Institution>
    {
        public override void Configure(EntityTypeBuilder<Institution> builder)
        {
            builder.ToTable("Instituicao");
            base.Configure(builder);

            builder
                .Property(m => m.Name)
                .HasMaxLength(80)
                .IsRequired()
                .HasColumnName("Nome");

            builder
                .Property(m => m.Code)
                .HasMaxLength(8)
                .HasDefaultValueSql("LEFT(LOWER(NEWID()), 8)")
                .IsRequired()
                .HasColumnName("Codigo");

            builder
                .Property(m => m.Image)
                .HasMaxLength(30)
                .IsRequired(false)
                .HasColumnName("Imagem");
        }
    }
}
