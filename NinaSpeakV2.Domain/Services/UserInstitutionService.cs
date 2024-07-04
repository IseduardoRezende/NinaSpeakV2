using AutoMapper;
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

        public async Task<ReadUserInstitutionViewModel> StandardRegistrationAsync(long userFk)
        {
            if (!BaseValidator.IsAbove(userFk, BaseValidator.IdMinValue))
                return new ReadUserInstitutionViewModel { BaseErrors = [new BaseError(BaseError.InvalidValue)] };

            var user = await _userRepository.GetByIdAsync(userFk);

            if (!BaseValidator.IsValid(user))            
                return new ReadUserInstitutionViewModel { BaseErrors = [new BaseError(BaseError.UserNotFound)] };

            var institution = await _institutionRepository.GetStandardAsync();
            
            var model = new UserInstitution 
            { 
                UserFk = user!.Id, 
                InstitutionFk = institution.Id,
                Owner = false,
                Writer = false
            };

            model = await _userInstitutionRepository.CreateAsync(model);
            return _mapper.Map<ReadUserInstitutionViewModel>(model);
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

        public async Task<IEnumerable<ReadUserInstitutionViewModel>> GetMembersByInstitutionFkAsync(long institutionFk)
        {
            var usersIntitution = await _userInstitutionRepository.GetMembersByInstitutionFkAsync(institutionFk);

            if (!BaseValidator.IsValid(usersIntitution))
                return Enumerable.Empty<ReadUserInstitutionViewModel>();

            return _mapper.Map<IEnumerable<ReadUserInstitutionViewModel>>(usersIntitution);
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

        protected override Task<IEnumerable<BaseError>> ValidateChangeAsync(UpdateUserInstitutionViewModel updateModel)
        {
            throw new NotImplementedException();
        }

        protected override void UpdateFields(UserInstitution model, UpdateUserInstitutionViewModel updateModel)
        {
            throw new NotImplementedException();
        }       
    }
}
