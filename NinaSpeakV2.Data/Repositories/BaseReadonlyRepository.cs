using Microsoft.EntityFrameworkCore;
using NinaSpeakV2.Data.Interfaces;
using NinaSpeakV2.Data.Repositories.IRepositories;

namespace NinaSpeakV2.Data.Repositories
{
    public abstract class BaseReadonlyRepository<TEntity> : IBaseReadonlyRepository<TEntity>
        where TEntity : class, IBaseEntityGlobal
    {
        protected NinaSpeakContext _context;

        protected BaseReadonlyRepository(NinaSpeakContext context)
        {
            _context = context;
        }

        protected DbSet<TEntity> Entity
        {
            get
            {
                return _context.Set<TEntity>();
            }
        }

        public virtual Task<IEnumerable<TEntity>> GetAsync(Func<TEntity, bool> filters, params string[] includes)
        {
            ArgumentNullException.ThrowIfNull(filters, nameof(filters));
            ArgumentNullException.ThrowIfNull(includes, nameof(includes));

            IQueryable<TEntity> query = Entity;

            foreach (var include in includes)
                query = query.Include(include);

            return Task.FromResult(query.Where(filters));
        }

        public virtual Task<TEntity?> GetByAsync(Func<TEntity, bool> filters, params string[] includes)
        {
            ArgumentNullException.ThrowIfNull(filters, nameof(filters));
            ArgumentNullException.ThrowIfNull(includes, nameof(includes));

            IQueryable<TEntity> query = Entity;

            foreach (var include in includes)
                query = query.Include(include);

            return Task.FromResult(query.Where(filters).FirstOrDefault());
        }

        public virtual async Task<TEntity?> GetByIdsAsync(long[] ids, params string[] includes)
        {
            ArgumentNullException.ThrowIfNull(ids, nameof(ids));
            ArgumentNullException.ThrowIfNull(includes, nameof(includes));

            if (!ids.Any() || ids.Length < 2 || ids.Any(v => v <= default(long)))
                return null;

            var keys = Array.ConvertAll(ids, id => (object)id);
            var model = await Entity.FindAsync(keys);

            if (model is null)
                return null;
            
            foreach (var include in includes)            
                _context.Entry(model).Reference(include).Load();            

            return model;
        }

        public virtual async Task<TEntity?> GetByIdAsync(long id, params string[] includes)
        {
            ArgumentNullException.ThrowIfNull(includes, nameof(includes));

            if (id <= default(long))
                return null;

            var model = await Entity.FindAsync(id);

            if (model is null)
                return null;

            foreach (var include in includes)
                _context.Entry(model).Reference(include).Load();

            return model;
        }
    }
}
