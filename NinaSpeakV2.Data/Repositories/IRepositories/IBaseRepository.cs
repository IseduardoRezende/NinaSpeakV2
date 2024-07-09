using NinaSpeakV2.Data.Interfaces;

namespace NinaSpeakV2.Data.Repositories.IRepositories
{
    public interface IBaseRepository<TModel> : IBaseReadonlyRepository<TModel>
        where TModel : class, IBaseModelGlobal
    {
        Task<TModel> CreateAsync(TModel model);
        
        Task<TModel> UpdateAsync(TModel model);

        Task<bool> SoftDeleteAsync(TModel model);
    }    
}
