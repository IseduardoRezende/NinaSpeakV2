using NinaSpeakV2.Data.Interfaces;

namespace NinaSpeakV2.Data.Repositories.IRepositories
{    
    public interface IBaseReadonlyRepository<TEntity>
        where TEntity : class, IBaseEntityGlobal
    {
        Task<IEnumerable<TEntity>> GetAsync(Func<TEntity, bool> filters, params string[] includes);

        Task<TEntity?> GetByIdsAsync(long[] ids, params string[] includes);
    
        Task<TEntity?> GetByIdAsync(long id, params string[] includes);   

        Task<TEntity?> GetByAsync(Func<TEntity, bool> filters, params string[] includes);
    }
}
