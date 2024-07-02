using NinaSpeakV2.Data.Models;
using NinaSpeakV2.Domain.ViewModels.UsersInstitutions;

namespace NinaSpeakV2.Domain.Services.IServices
{
    public interface IUserInstitutionService 
        : IBaseService<UserInstitution, CreateUserInstitutionViewModel, UpdateUserInstitutionViewModel, ReadUserInstitutionViewModel>
    {
        Task<ReadUserInstitutionViewModel> StandardRegistrationAsync(long userFk);

        Task<IEnumerable<ReadUserInstitutionViewModel>> GetByOwnerAsync(long userFk);

        Task<IEnumerable<ReadUserInstitutionViewModel>> GetByUserFkAsync(long userFk, bool onlyWriter = true);
    }
}
