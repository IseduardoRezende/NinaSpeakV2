using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NinaSpeakV2.Data.Models;

namespace NinaSpeakV2.Data.ModelsMapping
{
    public class InstitutionModelMapping : BaseModelEnumMapping<Institution>
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
                .Property(m => m.Image)
                .HasMaxLength(30)
                .IsRequired(false)
                .HasColumnName("Imagem");
        }
    }
}
