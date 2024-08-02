using AutoMapper;
using NinaSpeakV2.Data.Interfaces;
using NinaSpeakV2.Data.Repositories.IRepositories;
using NinaSpeakV2.Domain.Interfaces;
using NinaSpeakV2.Domain.Models;
using NinaSpeakV2.Domain.Services.IServices;
using NinaSpeakV2.Domain.Validators;

namespace NinaSpeakV2.Domain.Services
{
    public abstract class BaseReadonlyService<TEntity, TReadViewModel> : IBaseReadonlyService<TEntity, TReadViewModel>
        where TEntity        : class, IBaseEntityGlobal
        where TReadViewModel : class, IBaseReadViewModel, new()
    {
        protected IBaseReadonlyRepository<TEntity> _baseReadonlyRepository;
        protected IMapper _mapper;

        protected BaseReadonlyService(IBaseReadonlyRepository<TEntity> baseReadonlyRepository, IMapper mapper)
        {
            _baseReadonlyRepository = baseReadonlyRepository;
            _mapper = mapper;
        }

        public virtual async Task<IEnumerable<TReadViewModel>> GetAsync(params string[] includes)
        {
            if (!BaseValidator.IsValid(includes))
                return new[] { new TReadViewModel { BaseErrors = new[] { new BaseError(BaseError.InvalidIncludes) } } };

            var entities = await _baseReadonlyRepository.GetAsync(ApplyFilters(), includes);
            return _mapper.Map<IEnumerable<TReadViewModel>>(entities);
        }

        public virtual async Task<TReadViewModel?> GetByIdAsync(long id, params string[] includes)
        {
            if (!BaseValidator.IsAbove(id, BaseValidator.IdMinValue) || !BaseValidator.IsValid(includes))
                return null;

            var entity = await _baseReadonlyRepository.GetByIdAsync(id, includes);

            if (entity is null)
                return null;

            return _mapper.Map<TReadViewModel>(entity);
        }

        public virtual async Task<TReadViewModel?> GetByIdsAsync(long[] ids, params string[] includes)
        {
            if (!BaseValidator.IsValid(ids) || ids.Any(v => !BaseValidator.IsAbove(v, BaseValidator.IdMinValue)) ||
                !BaseValidator.IsValid(includes))
                return null;
        
            var entity = await _baseReadonlyRepository.GetByIdsAsync(ids, includes);

            if (entity is null)
                return null;

            return _mapper.Map<TReadViewModel>(entity);            
        }
        
        public virtual async Task<TReadViewModel?> GetByAsync(Func<TEntity, bool> filter, params string[] includes)
        {
            if (!BaseValidator.IsValid(filter))
                return new TReadViewModel { BaseErrors = new[] { new BaseError(BaseError.InvalidFilters) } };

            if (!BaseValidator.IsValid(includes))
                return new TReadViewModel { BaseErrors = new[] { new BaseError(BaseError.InvalidIncludes) } };

            var entity = await _baseReadonlyRepository.GetByAsync(filter, includes);

            if (entity is null)
                return null;
        
            return _mapper.Map<TReadViewModel>(entity);
        }

        protected abstract Func<TEntity, bool> ApplyFilters();
    }
}
