using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NinaSpeakV2.Data.Interfaces;

namespace NinaSpeakV2.Data.ModelsMapping
{
    public abstract class BaseModelGlobalMapping<TModel> : IEntityTypeConfiguration<TModel>
        where TModel : class, IBaseModelGlobal
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
