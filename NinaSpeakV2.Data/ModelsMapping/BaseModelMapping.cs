using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NinaSpeakV2.Data.Interfaces;

namespace NinaSpeakV2.Data.ModelsMapping
{
    public abstract class BaseModelMapping<TModel> : BaseModelGlobalMapping<TModel>
        where TModel : class, IBaseModel
    {
        public override void Configure(EntityTypeBuilder<TModel> builder)
        {
            base.Configure(builder);

            builder
                .Property(m => m.Id)
                .IsRequired()
                .HasColumnName("Id")
                .UseIdentityColumn();
            builder
                .HasKey(m => m.Id);           
        }
    }
}
