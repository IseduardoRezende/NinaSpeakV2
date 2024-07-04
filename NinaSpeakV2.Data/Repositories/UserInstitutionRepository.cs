using NinaSpeakV2.Data.Models;
using NinaSpeakV2.Data.Repositories.IRepositories;

namespace NinaSpeakV2.Data.Repositories
{
    public class UserInstitutionRepository : BaseRepository<UserInstitution>, IUserInstitutionRepository
    {
        public UserInstitutionRepository(NinaSpeakContext context) : base(context) { }

        public async Task<IEnumerable<UserInstitution>> GetMembersByInstitutionFkAsync(long institutionFk)
        {
            if (institutionFk <= default(long))
                return Enumerable.Empty<UserInstitution>();

            return await base.GetAsync(ui => ui.InstitutionFk == institutionFk, "User");
        }

        public async Task<IEnumerable<UserInstitution>> GetByOwnerAsync(long userFk)
        {
            if (userFk <= default(long))
                return Enumerable.Empty<UserInstitution>();

            return await base.GetAsync(ui => ui.UserFk == userFk && ui.Owner, "Institution");
        }

        public async Task<IEnumerable<UserInstitution>> GetByUserFkAsync(long userFk, bool onlyWriter = false)
        {
            if (userFk <= default(long))
                return Enumerable.Empty<UserInstitution>();

            if (onlyWriter)
            {
                return await base.GetAsync(ui => ui.UserFk == userFk && ui.Writer, "Institution");
            }

            return await base.GetAsync(ui => ui.UserFk == userFk, "Institution");
        }
    }
}
