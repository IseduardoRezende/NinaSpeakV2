using NinaSpeakV2.Data.Interfaces;

namespace NinaSpeakV2.Data.Repositories.IRepositories
{    
    public interface IBaseReadonlyRepository<TModel>
        where TModel : class, IBaseModelGlobal
    {
        Task<IEnumerable<TModel>> GetAsync(Func<TModel, bool> filters, params string[] includes);

        Task<TModel?> GetByIdsAsync(long[] ids, params string[] includes);
    
        Task<TModel?> GetByIdAsync(long id, params string[] includes);   

        Task<TModel?> GetByAsync(Func<TModel, bool> filters, params string[] includes);
    }
}
