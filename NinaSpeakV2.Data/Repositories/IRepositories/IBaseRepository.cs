using NinaSpeakV2.Data.Interfaces;

namespace NinaSpeakV2.Data.Repositories.IRepositories
{
    public interface IBaseRepository<TEntity> : IBaseReadonlyRepository<TEntity>
        where TEntity : class, IBaseEntityGlobal
    {
        Task<TEntity> CreateAsync(TEntity model);
        
        Task<TEntity> UpdateAsync(TEntity model);

        Task<bool> SoftDeleteAsync(TEntity model);

        Task<bool> ActiveAsync(TEntity model);
    }    
}
