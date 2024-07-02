using Microsoft.EntityFrameworkCore;
using NinaSpeakV2.Data.Interfaces;
using NinaSpeakV2.Data.Repositories.IRepositories;

namespace NinaSpeakV2.Data.Repositories
{
    public abstract class BaseReadonlyRepository<TModel> : IBaseReadonlyRepository<TModel>
        where TModel : class, IBaseModelGlobal
    {
        protected NinaSpeakContext _context;

        protected BaseReadonlyRepository(NinaSpeakContext context)
        {
            _context = context;
        }

        protected DbSet<TModel> Model
        {
            get
            {
                return _context.Set<TModel>();
            }
        }

        public virtual Task<IEnumerable<TModel>> GetAsync(Func<TModel, bool> filters, params string[] includes)
        {
            ArgumentNullException.ThrowIfNull(filters, nameof(filters));
            ArgumentNullException.ThrowIfNull(includes, nameof(includes));

            IQueryable<TModel> query = Model;

            foreach (var include in includes)
                query = query.Include(include);

            return Task.FromResult(query.Where(filters));
        }

        public virtual Task<TModel?> GetByAsync(Func<TModel, bool> filters, params string[] includes)
        {
            ArgumentNullException.ThrowIfNull(filters, nameof(filters));
            ArgumentNullException.ThrowIfNull(includes, nameof(includes));

            IQueryable<TModel> query = Model;

            foreach (var include in includes)
                query = query.Include(include);            

            return Task.FromResult(query.Where(filters).FirstOrDefault());
        }

        public virtual async Task<TModel?> GetByIdsAsync(params long[] ids)
        {
            ArgumentNullException.ThrowIfNull(ids, nameof(ids));
            
            if (!ids.Any() || ids.Any(v => v <= default(long)))
                return null;

            return await Model.FindAsync(ids);
        }

        public virtual async Task<TModel?> GetByIdAsync(long id)
        {
            if (id <= default(long))
                return null;

            return await Model.FindAsync(id);
        }
    }
}
