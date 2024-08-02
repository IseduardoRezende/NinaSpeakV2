using NinaSpeakV2.Data.Interfaces;
using NinaSpeakV2.Data.Repositories.IRepositories;

namespace NinaSpeakV2.Data.Repositories
{
    public abstract class BaseRepository<TEntity> : BaseReadonlyRepository<TEntity>, IBaseRepository<TEntity>
        where TEntity : class, IBaseEntityGlobal
    {
        protected BaseRepository(NinaSpeakContext context) : base(context) { }

        public virtual async Task<TEntity> CreateAsync(TEntity model)
        {
            ArgumentNullException.ThrowIfNull(model, nameof(model));

            var entryEntity = Entity.Add(model);

            if (!await SaveChangesAsync())
                throw new Exception();

            return entryEntity.Entity;
        }        

        public virtual async Task<TEntity> UpdateAsync(TEntity model)
        {
            ArgumentNullException.ThrowIfNull(model, nameof(model));

            var entryEntity = Entity.Update(model);

            if (!await SaveChangesAsync())
                throw new Exception();

            return entryEntity.Entity;
        }

        public virtual async Task<bool> SoftDeleteAsync(TEntity model)
        {
            if (model is null)
                return false;

            model.DeletedAt = DateTime.Now;
            Entity.Update(model);
            return await SaveChangesAsync();
        }

        public virtual async Task<bool> ActiveAsync(TEntity model)
        {
            if (model is null)
                return false;

            model.DeletedAt = null;
            Entity.Update(model);
            return await SaveChangesAsync();
        }

        protected async Task<bool> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync() > 0;
        }
    }
}
