using AutoMapper;
using NinaSpeakV2.Data.Interfaces;
using NinaSpeakV2.Data.Repositories.IRepositories;
using NinaSpeakV2.Domain.Entities;
using NinaSpeakV2.Domain.Interfaces;
using NinaSpeakV2.Domain.Services.IServices;
using NinaSpeakV2.Domain.Validators;

namespace NinaSpeakV2.Domain.Services
{
    public abstract class BaseReadonlyService<TModel, ReadModel> : IBaseReadonlyService<TModel, ReadModel>
        where TModel    : class, IBaseModelGlobal
        where ReadModel : class, IBaseReadViewModel, new()
    {
        protected IBaseReadonlyRepository<TModel> _baseReadonlyRepository;
        protected IMapper _mapper;

        protected BaseReadonlyService(IBaseReadonlyRepository<TModel> baseReadonlyRepository, IMapper mapper)
        {
            _baseReadonlyRepository = baseReadonlyRepository;
            _mapper = mapper;
        }

        public virtual async Task<IEnumerable<ReadModel>> GetAsync(params string[] includes)
        {
            if (!BaseValidator.IsValid(includes))
                return new[] { new ReadModel { BaseErrors = new[] { new BaseError(BaseError.InvalidIncludes) } } };

            var models = await _baseReadonlyRepository.GetAsync(ApplyFilters(), includes);
            return _mapper.Map<IEnumerable<ReadModel>>(models);
        }

        public virtual async Task<ReadModel?> GetByIdAsync(long id)
        {
            if (!BaseValidator.IsAbove(id, BaseValidator.IdMinValue))
                return null;

            var model = await _baseReadonlyRepository.GetByIdAsync(id);

            if (model is null)
                return null;

            return _mapper.Map<ReadModel>(model);
        }

        public virtual async Task<ReadModel?> GetByIdsAsync(params long[] ids)
        {
            if (!BaseValidator.IsValid(ids) || ids.Any(v => !BaseValidator.IsAbove(v, BaseValidator.IdMinValue)))
                return null;
        
            var model = await _baseReadonlyRepository.GetByIdsAsync(ids);

            if (model is null)
                return null;

            return _mapper.Map<ReadModel>(model);            
        }
        
        public virtual async Task<ReadModel?> GetByAsync(Func<TModel, bool> filter, params string[] includes)
        {
            if (!BaseValidator.IsValid(filter))
                return new ReadModel { BaseErrors = new[] { new BaseError(BaseError.InvalidFilters) } };

            if (!BaseValidator.IsValid(includes))
                return new ReadModel { BaseErrors = new[] { new BaseError(BaseError.InvalidIncludes) } };

            var model = await _baseReadonlyRepository.GetByAsync(filter, includes);

            if (model is null)
                return null;
        
            return _mapper.Map<ReadModel>(model);
        }

        protected abstract Func<TModel, bool> ApplyFilters();
    }
}
