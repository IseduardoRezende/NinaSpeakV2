using NinaSpeakV2.Data.Interfaces;

namespace NinaSpeakV2.Data.Repositories.IRepositories
{
    public interface IBaseRepository<TModel> : IBaseReadonlyRepository<TModel>
        where TModel : class, IBaseModelGlobal
    {
        public Task<TModel> CreateAsync(TModel model);
        
        public Task<TModel> UpdateAsync(TModel model);

        public Task<bool> SoftDeleteAsync(TModel model);
    }    
}
