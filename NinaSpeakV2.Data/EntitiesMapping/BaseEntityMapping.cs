using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NinaSpeakV2.Data.Interfaces;

namespace NinaSpeakV2.Data.EntitiesMapping
{
    public abstract class BaseEntityMapping<TModel> : BaseEntityGlobalMapping<TModel>
        where TModel : class, IBaseEntity
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
