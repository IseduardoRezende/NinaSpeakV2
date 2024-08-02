using NinaSpeakV2.Data.Entities;

namespace NinaSpeakV2.Data.Repositories.IRepositories
{
    public interface IUserInstitutionRepository : IBaseRepository<UserInstitution>
    {
        Task<IEnumerable<UserInstitution>> UpdateAsync(IEnumerable<UserInstitution> userInstitutions);

        Task<bool> SoftDeleteAsync(IEnumerable<UserInstitution> userInstitutions);

        Task<IEnumerable<UserInstitution>> GetByOwnerAsync(long userFk);

        Task<IEnumerable<UserInstitution>> GetByUserFkAsync(long userFk, bool onlyWriter = false);

        Task<IEnumerable<UserInstitution>> GetMembersByInstitutionFkAsync(long institutionFk);
    }
}
