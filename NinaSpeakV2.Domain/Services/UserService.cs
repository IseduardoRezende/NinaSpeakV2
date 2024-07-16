using AutoMapper;
using NinaSpeakV2.Data.Models;
using NinaSpeakV2.Data.Repositories.IRepositories;
using NinaSpeakV2.Domain.Entities;
using NinaSpeakV2.Domain.Extensions;
using NinaSpeakV2.Domain.Services.IServices;
using NinaSpeakV2.Domain.Validators;
using NinaSpeakV2.Domain.ViewModels.Users;

namespace NinaSpeakV2.Domain.Services
{
    public class UserService : BaseService<User, CreateUserViewModel, UpdateUserViewModel, ReadUserViewModel>, IUserService
    {
        protected readonly IUserRepository _userRepository;
        protected readonly IUserInstitutionService _userInstitutionService;

        public UserService(IUserRepository userRepository, IUserInstitutionService userInstitutionService,
            IMapper mapper) : base(userRepository, mapper)
        {
            _userRepository = userRepository;
            _userInstitutionService = userInstitutionService;
        }

        //TODO: DELETE ACTION

        public override async Task<ReadUserViewModel> CreateAsync(CreateUserViewModel createModel)
        {
            var errors = await ValidateCreationAsync(createModel);

            if (errors.Any())
                return new ReadUserViewModel { BaseErrors = errors };

            createModel.Salt = Guid.NewGuid().ToString();
            createModel.Password = createModel.Password.ConvertToSHA512(createModel.Salt);
            createModel.Email = createModel.Email.ToLowerInvariant();

            var model = _mapper.Map<User>(createModel);
            model = await _userRepository.CreateAsync(model);
            
            var userInstitution = await _userInstitutionService.StandardRegistrationAsync(model.Id);

            if (userInstitution.HasErrors())
                return new ReadUserViewModel { BaseErrors = userInstitution.BaseErrors };

            //Send Email to Confirm

            return _mapper.Map<ReadUserViewModel>(model);
        }

        public override async Task<ReadUserViewModel> UpdateAsync(UpdateUserViewModel updateModel)
        {
            var errors = await ValidateChangeAsync(updateModel);

            if (errors.Any())
                return new ReadUserViewModel { BaseErrors = errors };

            var model = await _userRepository.GetByIdAsync(updateModel.Id);

            if (!BaseValidator.IsValid(model))
                return new ReadUserViewModel { BaseErrors = new[] { new BaseError(BaseError.InexistentObject) } };

            UpdateFields(model!, updateModel);
            model = await _userRepository.UpdateAsync(model!);
            
            //Send Email to Confirm
            
            return _mapper.Map<ReadUserViewModel>(model);
        }

        protected override Func<User, bool> ApplyFilters()
        {
            return _ => true;
        }

        protected override async Task<IEnumerable<BaseError>> ValidateCreationAsync(CreateUserViewModel createModel)
        {
            var errors = new List<BaseError>();            

            if (!BaseValidator.IsValid(createModel))
            {
                errors.Add(new BaseError(BaseError.NullObject));
                return errors;
            }

            if (createModel.Password != createModel.ConfirmPassword)
                errors.Add(new BaseError(BaseError.PasswordNotMatch));

            if (!UserValidator.IsValidPassword(createModel.Password))
                errors.Add(new BaseError(BaseError.InvalidPassword));

            if (!UserValidator.IsValidEmail(createModel.Email))
                errors.Add(new BaseError(BaseError.InvalidEmail));

            if (await _userRepository.EmailAlreadyExistAsync(createModel.Email))
                errors.Add(new BaseError(BaseError.EmailAlreadyExist));

            return errors;
        }

        protected override async Task<IEnumerable<BaseError>> ValidateChangeAsync(UpdateUserViewModel updateModel)
        {
            var errors = new List<BaseError>();

            if (!BaseValidator.IsValid(updateModel))
            {
                errors.Add(new BaseError(BaseError.NullObject));
                return errors;
            }
            
            if (updateModel.Password != updateModel.ConfirmPassword)
                errors.Add(new BaseError(BaseError.PasswordNotMatch));

            if (!UserValidator.IsValidPassword(updateModel.Password))
                errors.Add(new BaseError(BaseError.InvalidPassword));

            if (!UserValidator.IsValidEmail(updateModel.Email))
                errors.Add(new BaseError(BaseError.InvalidEmail));

            var user = await _userRepository.GetByIdAsync(updateModel.Id);

            if (!BaseValidator.IsValid(user))
            {
                errors.Add(new BaseError(BaseError.UserNotFound));
                return errors;
            }

            if (user!.Email.Equals(updateModel.Email, StringComparison.InvariantCultureIgnoreCase)) //Create an abstract implementation (IsEqual) foreach [Model]Validator
                errors.Add(new BaseError(BaseError.NoChangesDetected));

            var password = updateModel.Password.ConvertToSHA512(user.Salt);

            if (user.Password != password)
                errors.Add(new BaseError(BaseError.InvalidPassword));

            if (!await _userRepository.CanChangeEmailAsync(user, updateModel.Email))
                errors.Add(new BaseError(BaseError.EmailAlreadyExist));

            return errors;
        }

        protected override void UpdateFields(User model, UpdateUserViewModel updateModel)
        {
            model.Email = updateModel.Email.ToLowerInvariant();
        
            //Activated = false
        }
    }
}
