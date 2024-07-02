using NinaSpeakV2.Data.Interfaces;
using NinaSpeakV2.Domain.Interfaces;

namespace NinaSpeakV2.Domain.Services.IServices
{
    public interface IBaseReadonlyService<TModel, ReadModel>
        where TModel    : class, IBaseModelGlobal
        where ReadModel : class, IBaseReadViewModel, new()
    {
        public Task<IEnumerable<ReadModel>> GetAsync(params string[] includes);
        
        public Task<ReadModel?> GetByIdsAsync(params long[] ids);

        public Task<ReadModel?> GetByIdAsync(long id);

        Task<ReadModel?> GetByAsync(Func<TModel, bool> filter, params string[] includes);
    }
}
