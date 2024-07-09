using NinaSpeakV2.Data.Interfaces;
using NinaSpeakV2.Domain.Interfaces;

namespace NinaSpeakV2.Domain.Services.IServices
{
    public interface IBaseService<TModel, CreateModel, UpdateModel, ReadModel> : IBaseReadonlyService<TModel, ReadModel>        
        where TModel      : class, IBaseModelGlobal
        where CreateModel : class, IBaseCreateViewModel
        where UpdateModel : class, IBaseUpdateViewModel
        where ReadModel   : class, IBaseReadViewModel, new()
    {
        Task<ReadModel> CreateAsync(CreateModel createModel);
        
        Task<ReadModel> UpdateAsync(UpdateModel updateModel);

        Task<bool> SoftDeleteAsync(long id);

        Task<bool> SoftDeleteAsync(params long[] ids);
    }
}
