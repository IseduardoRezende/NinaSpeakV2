using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NinaSpeakV2.Data.Interfaces;

namespace NinaSpeakV2.Data.EntitiesMapping
{
    public abstract class BaseEntityGlobalMapping<TModel> : IEntityTypeConfiguration<TModel>
        where TModel : class, IBaseEntityGlobal
    {
        public virtual void Configure(EntityTypeBuilder<TModel> builder)
        {
            builder
               .Property(m => m.CreatedAt)
               .HasDefaultValueSql("GETDATE()")
               .IsRequired()
               .HasColumnName("DataCriacao");

            builder
                .Property(m => m.DeletedAt)
                .IsRequired(false)
                .HasColumnName("DataExclusao");

            builder.HasQueryFilter(m => m.DeletedAt == null);
        }
    }
}
