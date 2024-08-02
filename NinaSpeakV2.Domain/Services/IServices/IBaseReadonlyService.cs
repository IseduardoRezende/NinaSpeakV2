using NinaSpeakV2.Data.Interfaces;
using NinaSpeakV2.Domain.Interfaces;

namespace NinaSpeakV2.Domain.Services.IServices
{
    public interface IBaseReadonlyService<TEntity, TReadViewModel>
        where TEntity        : class, IBaseEntityGlobal
        where TReadViewModel : class, IBaseReadViewModel, new()
    {
        Task<IEnumerable<TReadViewModel>> GetAsync(params string[] includes);
        
        Task<TReadViewModel?> GetByIdsAsync(long[] ids, params string[] includes);

        Task<TReadViewModel?> GetByIdAsync(long id, params string[] includes);

        Task<TReadViewModel?> GetByAsync(Func<TEntity, bool> filter, params string[] includes);
    }
}
