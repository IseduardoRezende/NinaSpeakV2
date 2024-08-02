using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NinaSpeakV2.Data.Interfaces;

namespace NinaSpeakV2.Data.EntitiesMapping
{
    public abstract class BaseEntityEnumMapping<TModel> : BaseEntityMapping<TModel>
        where TModel : class, IBaseEntityEnum
    {
        public override void Configure(EntityTypeBuilder<TModel> builder)
        {
            base.Configure(builder);

            builder
                .Property(m => m.Description)
                .HasMaxLength(150)
                .IsRequired(false)
                .HasColumnName("Descricao");
        }
    }
}
