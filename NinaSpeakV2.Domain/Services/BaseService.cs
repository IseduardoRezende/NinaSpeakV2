using AutoMapper;
using NinaSpeakV2.Data.Interfaces;
using NinaSpeakV2.Data.Repositories.IRepositories;
using NinaSpeakV2.Domain.Entities;
using NinaSpeakV2.Domain.Interfaces;
using NinaSpeakV2.Domain.Services.IServices;
using NinaSpeakV2.Domain.Validators;

namespace NinaSpeakV2.Domain.Services
{
    public abstract class BaseService<TModel, CreateModel, UpdateModel, ReadModel> : BaseReadonlyService<TModel, ReadModel>, 
                                                                                     IBaseService<TModel, CreateModel, UpdateModel, ReadModel>
        where TModel      : class, IBaseModelGlobal
        where CreateModel : class, IBaseCreateViewModel
        where UpdateModel : class, IBaseUpdateViewModel
        where ReadModel   : class, IBaseReadViewModel, new()
    {
        protected IBaseRepository<TModel> _baseRepository;

        protected BaseService(IBaseRepository<TModel> baseRepository, IMapper mapper) : base(baseRepository, mapper)
        {
            _baseRepository = baseRepository;
        }
        
        protected abstract Task<IEnumerable<BaseError>> ValidateCreationAsync(CreateModel createModel);
        
        public virtual async Task<ReadModel> CreateAsync(CreateModel createModel)
        {
            var errors = await ValidateCreationAsync(createModel);

            if (errors.Any())
                return new ReadModel { BaseErrors = errors };

            var model = _mapper.Map<TModel>(createModel);
            model = await _baseRepository.CreateAsync(model);
            return _mapper.Map<ReadModel>(model);
        }

        protected abstract Task<IEnumerable<BaseError>> ValidateChangeAsync(UpdateModel updateModel);

        public virtual async Task<ReadModel> UpdateAsync(UpdateModel updateModel)
        {
            var errors = await ValidateChangeAsync(updateModel);

            if (errors.Any())
                return new ReadModel { BaseErrors = errors };

            var model = await _baseReadonlyRepository.GetByIdAsync(updateModel.Id);

            if (!BaseValidator.IsValid(model))
                return new ReadModel { BaseErrors = new[] { new BaseError(BaseError.InexistentObject) } };

            UpdateFields(model!, updateModel);

            model = await _baseRepository.UpdateAsync(model!);
            return _mapper.Map<ReadModel>(model);
        }

        public virtual async Task<bool> SoftDeleteAsync(long id)
        {
            if (!BaseValidator.IsAbove(id, BaseValidator.IdMinValue))
                return false;

            var model = await _baseReadonlyRepository.GetByIdAsync(id);

            if (!BaseValidator.IsValid(model))
                return false;

            return await _baseRepository.SoftDeleteAsync(model!);
        }

        public virtual async Task<bool> SoftDeleteAsync(params long[] ids)
        {
            if (!BaseValidator.IsValid(ids) || ids.Any(v => !BaseValidator.IsAbove(v, BaseValidator.IdMinValue)))
                return false;

            var model = await _baseReadonlyRepository.GetByIdsAsync(ids);

            if (!BaseValidator.IsValid(model))
                return false;

            return await _baseRepository.SoftDeleteAsync(model!);
        }

        public virtual async Task<bool> ActiveAsync(long id)
        {
            if (!BaseValidator.IsAbove(id, BaseValidator.IdMinValue))
                return false;

            var model = await _baseReadonlyRepository.GetByIdAsync(id);

            if (!BaseValidator.IsValid(model))
                return false;

            return await _baseRepository.ActiveAsync(model!);
        }

        public virtual async Task<bool> ActiveAsync(params long[] ids)
        {
            if (!BaseValidator.IsValid(ids) || ids.Any(v => !BaseValidator.IsAbove(v, BaseValidator.IdMinValue)))
                return false;

            var model = await _baseReadonlyRepository.GetByIdsAsync(ids);

            if (!BaseValidator.IsValid(model))
                return false;

            return await _baseRepository.ActiveAsync(model!);
        }

        protected abstract void UpdateFields(TModel model, UpdateModel updateModel);
    }
}
