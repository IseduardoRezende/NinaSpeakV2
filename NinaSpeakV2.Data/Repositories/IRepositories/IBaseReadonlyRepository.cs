using NinaSpeakV2.Data.Interfaces;

namespace NinaSpeakV2.Data.Repositories.IRepositories
{    
    public interface IBaseReadonlyRepository<TModel>
        where TModel : class, IBaseModelGlobal
    {
        public Task<IEnumerable<TModel>> GetAsync(Func<TModel, bool> filters, params string[] includes);

        public Task<TModel?> GetByIdsAsync(long[] ids, params string[] includes);
    
        public Task<TModel?> GetByIdAsync(long id, params string[] includes);   

        public Task<TModel?> GetByAsync(Func<TModel, bool> filters, params string[] includes);
    }
}
