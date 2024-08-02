using AutoMapper;
using NinaSpeakV2.Data.Interfaces;
using NinaSpeakV2.Data.Repositories.IRepositories;
using NinaSpeakV2.Domain.Interfaces;
using NinaSpeakV2.Domain.Models;
using NinaSpeakV2.Domain.Services.IServices;
using NinaSpeakV2.Domain.Validators;

namespace NinaSpeakV2.Domain.Services
{
    public abstract class BaseService<TEntity, TCreateViewModel, TUpdateViewModel, TReadViewModel> : 
                          BaseReadonlyService<TEntity, TReadViewModel>, IBaseService<TEntity, TCreateViewModel, TUpdateViewModel, TReadViewModel>
        where TEntity          : class, IBaseEntityGlobal
        where TCreateViewModel : class, IBaseCreateViewModel
        where TUpdateViewModel : class, IBaseUpdateViewModel
        where TReadViewModel   : class, IBaseReadViewModel, new()
    {
        protected IBaseRepository<TEntity> _baseRepository;

        protected BaseService(IBaseRepository<TEntity> baseRepository, IMapper mapper) : base(baseRepository, mapper)
        {
            _baseRepository = baseRepository;
        }
        
        protected abstract Task<IEnumerable<BaseError>> ValidateCreationAsync(TCreateViewModel createViewModel);
        
        public virtual async Task<TReadViewModel> CreateAsync(TCreateViewModel createViewModel)
        {
            var errors = await ValidateCreationAsync(createViewModel);

            if (errors.Any())
                return new TReadViewModel { BaseErrors = errors };

            var entity = _mapper.Map<TEntity>(createViewModel);
            entity = await _baseRepository.CreateAsync(entity);
            return _mapper.Map<TReadViewModel>(entity);
        }

        protected abstract Task<IEnumerable<BaseError>> ValidateChangeAsync(TUpdateViewModel updateViewModel);

        public virtual async Task<TReadViewModel> UpdateAsync(TUpdateViewModel updateViewModel)
        {
            var errors = await ValidateChangeAsync(updateViewModel);

            if (errors.Any())
                return new TReadViewModel { BaseErrors = errors };

            var entity = await _baseReadonlyRepository.GetByIdAsync(updateViewModel.Id);

            if (!BaseValidator.IsValid(entity))
                return new TReadViewModel { BaseErrors = new[] { new BaseError(BaseError.InexistentObject) } };

            UpdateFields(entity!, updateViewModel);

            entity = await _baseRepository.UpdateAsync(entity!);
            return _mapper.Map<TReadViewModel>(entity);
        }

        public virtual async Task<bool> SoftDeleteAsync(long id)
        {
            if (!BaseValidator.IsAbove(id, BaseValidator.IdMinValue))
                return false;

            var entity = await _baseReadonlyRepository.GetByIdAsync(id);

            if (!BaseValidator.IsValid(entity) || entity!.DeletedAt is not null)
                return false;

            return await _baseRepository.SoftDeleteAsync(entity!);
        }

        public virtual async Task<bool> SoftDeleteAsync(params long[] ids)
        {
            if (!BaseValidator.IsValid(ids) || ids.Any(v => !BaseValidator.IsAbove(v, BaseValidator.IdMinValue)))
                return false;

            var entity = await _baseReadonlyRepository.GetByIdsAsync(ids);

            if (!BaseValidator.IsValid(entity) || entity!.DeletedAt is not null)
                return false;

            return await _baseRepository.SoftDeleteAsync(entity!);
        }

        public virtual async Task<bool> ActiveAsync(long id)
        {
            if (!BaseValidator.IsAbove(id, BaseValidator.IdMinValue))
                return false;

            var entity = await _baseReadonlyRepository.GetByIdAsync(id);

            if (!BaseValidator.IsValid(entity) || entity!.DeletedAt is null)
                return false;

            return await _baseRepository.ActiveAsync(entity!);
        }

        public virtual async Task<bool> ActiveAsync(params long[] ids)
        {
            if (!BaseValidator.IsValid(ids) || ids.Any(v => !BaseValidator.IsAbove(v, BaseValidator.IdMinValue)))
                return false;

            var entity = await _baseReadonlyRepository.GetByIdsAsync(ids);

            if (!BaseValidator.IsValid(entity) || entity!.DeletedAt is null)
                return false;

            return await _baseRepository.ActiveAsync(entity!);
        }

        protected abstract void UpdateFields(TEntity entity, TUpdateViewModel updateViewModel);
    }
}
