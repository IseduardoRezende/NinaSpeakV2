using AutoMapper;
using Microsoft.Extensions.Caching.Memory;
using NinaSpeakV2.Data.Models;
using NinaSpeakV2.Data.Repositories.IRepositories;
using NinaSpeakV2.Domain.Entities;
using NinaSpeakV2.Domain.Services.IServices;
using NinaSpeakV2.Domain.Validators;
using NinaSpeakV2.Domain.ViewModels.UsersInstitutions;

namespace NinaSpeakV2.Domain.Services
{
    public class UserInstitutionService : BaseService<UserInstitution, CreateUserInstitutionViewModel, UpdateUserInstitutionViewModel, ReadUserInstitutionViewModel>,
                                          IUserInstitutionService
    {
        private readonly static IMemoryCache _membersCache = new MemoryCache(new MemoryCacheOptions());

        private readonly IUserRepository _userRepository;
        private readonly IInstitutionRepository _institutionRepository;
        private readonly IUserInstitutionRepository _userInstitutionRepository;

        public UserInstitutionService(IUserInstitutionRepository userInstitutionRepository, IUserRepository userRepository,
                                      IInstitutionRepository institutionRepository, IMapper mapper) : base(userInstitutionRepository, mapper)
        {
            _userRepository = userRepository;
            _institutionRepository = institutionRepository;
            _userInstitutionRepository = userInstitutionRepository;
        }

        public override async Task<ReadUserInstitutionViewModel> CreateAsync(CreateUserInstitutionViewModel createModel)
        {
            var result = await base.CreateAsync(createModel);

            if (result?.BaseErrors?.Any() ?? false)
                return result;

            _membersCache.Set(result!.InstitutionFk, new[] { result }.AsEnumerable());
            return result;
        }

        public async Task<IEnumerable<ReadUserInstitutionViewModel>> UpdateAsync(IEnumerable<UpdateUserInstitutionViewModel> updateModels)
        {
            var errors = await ValidateChangeAsync(updateModels);

            if (errors.Any())
                return new[] { new ReadUserInstitutionViewModel { BaseErrors = errors } };

            var changedOnes = await GetOnlyChangedOnesAsync(updateModels);

            if (!BaseValidator.IsValid(changedOnes))
                return new[] { new ReadUserInstitutionViewModel { BaseErrors = new[] { new BaseError(BaseError.NoChangesDetected) } } };

            var models = _mapper.Map<IEnumerable<UserInstitution>>(changedOnes);

            models = await _userInstitutionRepository.UpdateAsync(models);

            var readModels = _mapper.Map<IEnumerable<ReadUserInstitutionViewModel>>(models);

            _membersCache.Set(updateModels.First().InstitutionFk, readModels);

            return readModels;
        }

        private async Task<IEnumerable<UpdateUserInstitutionViewModel>> GetOnlyChangedOnesAsync(IEnumerable<UpdateUserInstitutionViewModel> updateModels)
        {
            if (!BaseValidator.IsValid(updateModels))
                return Enumerable.Empty<UpdateUserInstitutionViewModel>();

            var usersInstitutions = await _userInstitutionRepository.GetMembersByInstitutionFkAsync(updateModels.First().InstitutionFk);

            if (!BaseValidator.IsValid(usersInstitutions))
                return Enumerable.Empty<UpdateUserInstitutionViewModel>();

            var changedOnes = new List<UpdateUserInstitutionViewModel>();

            foreach (var updateModel in updateModels)
            {
                var userInstitution = usersInstitutions.FirstOrDefault(ui => ui.UserFk == updateModel.UserFk);

                if (!BaseValidator.IsValid(userInstitution))
                    return Enumerable.Empty<UpdateUserInstitutionViewModel>();

                if (userInstitution!.Creator || UserInstitutionValidator.IsEqual(userInstitution, updateModel))
                    continue;

                changedOnes.Add(updateModel);
            }

            return changedOnes;
        }

        private async Task<IEnumerable<BaseError>> ValidateChangeAsync(IEnumerable<UpdateUserInstitutionViewModel> updateModels)
        {
            var errors = new List<BaseError>();

            if (!BaseValidator.IsValid(updateModels))
            {
                errors.Add(new BaseError(BaseError.NullObject));
                return errors;
            }

            var updateModel = updateModels.First();

            if (!updateModels.All(ui => ui.InstitutionFk == updateModel.InstitutionFk))
            {
                errors.Add(new BaseError(BaseError.InvalidValue));
                return errors;
            }

            var institution = await _institutionRepository.GetByIdAsync(updateModel.InstitutionFk);

            if (!BaseValidator.IsValid(institution))
                errors.Add(new BaseError(BaseError.InstitutionNotFound));

            return errors;
        }

        public async Task<ReadUserInstitutionViewModel> StandardRegistrationAsync(long userFk)
        {
            if (!BaseValidator.IsAbove(userFk, BaseValidator.IdMinValue))
                return new ReadUserInstitutionViewModel { BaseErrors = [new BaseError(BaseError.InvalidValue)] };

            var user = await _userRepository.GetByIdAsync(userFk);

            if (!BaseValidator.IsValid(user))
                return new ReadUserInstitutionViewModel { BaseErrors = [new BaseError(BaseError.UserNotFound)] };

            var institution = await _institutionRepository.GetStandardAsync();

            var userInstitution = new UserInstitution
            {
                UserFk = user!.Id,
                InstitutionFk = institution.Id,
                Owner = false,
                Writer = false,
                Creator = false
            };

            userInstitution = await _userInstitutionRepository.CreateAsync(userInstitution);
            var readModel = _mapper.Map<ReadUserInstitutionViewModel>(userInstitution);

            var usersInstitution = await GetMembersByInstitutionFkAsync(institution.Id);
            _membersCache.Set(institution.Id, usersInstitution.Append(readModel));

            return readModel;
        }

        protected override Func<UserInstitution, bool> ApplyFilters()
        {
            return _ => true;
        }

        protected override async Task<IEnumerable<BaseError>> ValidateCreationAsync(CreateUserInstitutionViewModel createModel)
        {
            var errors = new List<BaseError>();

            if (!BaseValidator.IsValid(createModel))
            {
                errors.Add(new BaseError(BaseError.NullObject));
                return errors;
            }

            if (!BaseValidator.IsAbove(createModel.UserFk, BaseValidator.IdMinValue))
            {
                errors.Add(new BaseError(BaseError.InvalidValue));
                return errors;
            }

            if (!BaseValidator.IsAbove(createModel.InstitutionFk, BaseValidator.IdMinValue))
            {
                errors.Add(new BaseError(BaseError.InvalidValue));
                return errors;
            }

            var user = await _userRepository.GetByIdAsync(createModel.UserFk);

            if (!BaseValidator.IsValid(user))
            {
                errors.Add(new BaseError(BaseError.UserNotFound));
                return errors;
            }

            var institution = await _institutionRepository.GetByIdAsync(createModel.InstitutionFk);

            if (!BaseValidator.IsValid(institution))
            {
                errors.Add(new BaseError(BaseError.InstitutionNotFound));
                return errors;
            }

            return errors;
        }

        public async Task<bool> SoftDeleteByInstitutionFkAsync(long institutionFk)
        {
            if (!BaseValidator.IsAbove(institutionFk, BaseValidator.IdMinValue))
                return false;

            var readModels = await GetMembersByInstitutionFkAsync(institutionFk);

            if (!BaseValidator.IsValid(readModels))
                return false;

            var userInstitutions = _mapper.Map<IEnumerable<UserInstitution>>(readModels);

            if (!await _userInstitutionRepository.SoftDeleteAsync(userInstitutions))
                return false;

            _membersCache.Remove(institutionFk);
            return true;
        }

        public async Task<bool> SoftDeleteAsync(long userFk, long institutionFk)
        {
            if (!await base.SoftDeleteAsync(userFk, institutionFk))
                return false;

            if (!_membersCache.TryGetValue(institutionFk, out IEnumerable<ReadUserInstitutionViewModel>? members))
                return false;
            
            if ((members!.LongCount() - 1) is 0)
            {
                var institution = await _institutionRepository.GetByIdAsync(institutionFk);

                if (!await _institutionRepository.SoftDeleteAsync(institution!))
                    return false;

                _membersCache.Remove(institutionFk);
                return true;
            }

            var updatedMembers = members!.Where(ui => ui.UserFk != userFk);

            if (!updatedMembers.Any(m => m.Creator) && !updatedMembers.Any(m => m.Owner))
                await ChooseNewOwner(updatedMembers);

            _membersCache.Set(institutionFk, updatedMembers);
            return true;
        }

        private async Task ChooseNewOwner(IEnumerable<ReadUserInstitutionViewModel> members)
        {
            if (!BaseValidator.IsValid(members))
                return;

            var oldestMember = members.MinBy(m => m.CreatedAt);
            oldestMember!.Owner = true;

            var model = _mapper.Map<UserInstitution>(oldestMember);

            await _userInstitutionRepository.UpdateAsync(model);           
        }

        public async Task<IEnumerable<ReadUserInstitutionViewModel>> GetMembersByInstitutionFkAsync(long institutionFk)
        {
            if (_membersCache.TryGetValue(institutionFk, out IEnumerable<ReadUserInstitutionViewModel>? values))
                return values!;

            var usersIntitution = await _userInstitutionRepository.GetMembersByInstitutionFkAsync(institutionFk);

            if (!BaseValidator.IsValid(usersIntitution))
                return Enumerable.Empty<ReadUserInstitutionViewModel>();

            var readModels = _mapper.Map<IEnumerable<ReadUserInstitutionViewModel>>(usersIntitution);

            _membersCache.Set(institutionFk, readModels);

            return readModels;
        }

        public async Task<IEnumerable<ReadUserInstitutionViewModel>> GetByOwnerAsync(long userFk)
        {
            var userInstitutions = await _userInstitutionRepository.GetByOwnerAsync(userFk);

            if (!BaseValidator.IsValid(userInstitutions))
                return Enumerable.Empty<ReadUserInstitutionViewModel>();

            return _mapper.Map<IEnumerable<ReadUserInstitutionViewModel>>(userInstitutions);
        }

        public async Task<IEnumerable<ReadUserInstitutionViewModel>> GetByUserFkAsync(long userFk, bool onlyWriter = false)
        {
            var userInstitution = await _userInstitutionRepository.GetByUserFkAsync(userFk, onlyWriter);

            if (!BaseValidator.IsValid(userInstitution))
                return Enumerable.Empty<ReadUserInstitutionViewModel>();

            return _mapper.Map<IEnumerable<ReadUserInstitutionViewModel>>(userInstitution);
        }

        protected override async Task<IEnumerable<BaseError>> ValidateChangeAsync(UpdateUserInstitutionViewModel updateModel)
        {
            var errors = new List<BaseError>();

            if (!BaseValidator.IsValid(updateModel))
            {
                errors.Add(new BaseError(BaseError.NullObject));
                return errors;
            }

            if (!BaseValidator.IsAbove(updateModel.UserFk, BaseValidator.IdMinValue))
            {
                errors.Add(new BaseError(BaseError.InvalidValue));
                return errors;
            }

            if (!BaseValidator.IsAbove(updateModel.InstitutionFk, BaseValidator.IdMinValue))
            {
                errors.Add(new BaseError(BaseError.InvalidValue));
                return errors;
            }

            var userInstitution = await _userInstitutionRepository.GetByIdsAsync(new[] { updateModel.UserFk, updateModel.InstitutionFk });

            if (!BaseValidator.IsValid(userInstitution))
            {
                errors.Add(new BaseError(BaseError.UserInstitutionNotFound));
                return errors;
            }

            if (UserInstitutionValidator.IsEqual(userInstitution!, updateModel))
                errors.Add(new BaseError(BaseError.NoChangesDetected));

            return errors;
        }

        protected override void UpdateFields(UserInstitution model, UpdateUserInstitutionViewModel updateModel)
        {
            model.Owner = updateModel.Owner;
            model.Writer = updateModel.Writer;
        }
    }
}
