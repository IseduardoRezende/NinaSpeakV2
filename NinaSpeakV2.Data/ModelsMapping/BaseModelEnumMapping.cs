using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NinaSpeakV2.Data.Interfaces;

namespace NinaSpeakV2.Data.ModelsMapping
{
    public abstract class BaseModelEnumMapping<TModel> : BaseModelMapping<TModel>
        where TModel : class, IBaseModelEnum
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
