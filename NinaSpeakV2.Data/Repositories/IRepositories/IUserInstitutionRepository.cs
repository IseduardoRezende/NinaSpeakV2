using NinaSpeakV2.Data.Models;

namespace NinaSpeakV2.Data.Repositories.IRepositories
{
    public interface IUserInstitutionRepository : IBaseRepository<UserInstitution>
    {
        Task<IEnumerable<UserInstitution>> GetByOwnerAsync(long userFk);

        Task<IEnumerable<UserInstitution>> GetByUserFkAsync(long userFk, bool onlyWriter = false);

        Task<IEnumerable<UserInstitution>> GetMembersByInstitutionFkAsync(long institutionFk);
    }
}
