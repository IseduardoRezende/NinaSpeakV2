using NinaSpeakV2.Data.Interfaces;
using NinaSpeakV2.Domain.Interfaces;

namespace NinaSpeakV2.Domain.Services.IServices
{
    public interface IBaseService<TEntity, TCreateViewModel, TUpdateViewModel, TReadViewModel> : IBaseReadonlyService<TEntity, TReadViewModel>        
        where TEntity          : class, IBaseEntityGlobal
        where TCreateViewModel : class, IBaseCreateViewModel
        where TUpdateViewModel : class, IBaseUpdateViewModel
        where TReadViewModel   : class, IBaseReadViewModel, new()
    {
        Task<TReadViewModel> CreateAsync(TCreateViewModel createModel);
        
        Task<TReadViewModel> UpdateAsync(TUpdateViewModel updateModel);

        Task<bool> SoftDeleteAsync(long id);

        Task<bool> SoftDeleteAsync(params long[] ids);

        Task<bool> ActiveAsync(long id);

        Task<bool> ActiveAsync(params long[] ids);
    }
}
