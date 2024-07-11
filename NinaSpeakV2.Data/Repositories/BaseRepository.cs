using NinaSpeakV2.Data.Interfaces;
using NinaSpeakV2.Data.Repositories.IRepositories;

namespace NinaSpeakV2.Data.Repositories
{
    public abstract class BaseRepository<TModel> : BaseReadonlyRepository<TModel>, IBaseRepository<TModel>
        where TModel : class, IBaseModelGlobal
    {
        protected BaseRepository(NinaSpeakContext context) : base(context) { }

        public virtual async Task<TModel> CreateAsync(TModel model)
        {
            ArgumentNullException.ThrowIfNull(model, nameof(model));

            var entryEntity = Model.Add(model);

            if (!await SaveChangesAsync())
                throw new Exception();

            return entryEntity.Entity;
        }        

        public virtual async Task<TModel> UpdateAsync(TModel model)
        {
            ArgumentNullException.ThrowIfNull(model, nameof(model));

            var entryEntity = Model.Update(model);

            if (!await SaveChangesAsync())
                throw new Exception();

            return entryEntity.Entity;
        }

        public virtual async Task<bool> SoftDeleteAsync(TModel model)
        {
            if (model is null)
                return false;

            model.DeletedAt = DateTime.Now;
            Model.Update(model);
            return await SaveChangesAsync();
        }

        public virtual async Task<bool> ActiveAsync(TModel model)
        {
            if (model is null)
                return false;

            model.DeletedAt = null;
            Model.Update(model);
            return await SaveChangesAsync();
        }

        protected async Task<bool> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync() > 0;
        }
    }
}
