using NinaSpeakV2.Data.Models;
using NinaSpeakV2.Data.Repositories.IRepositories;

namespace NinaSpeakV2.Data.Repositories
{
    public class UserInstitutionRepository : BaseRepository<UserInstitution>, IUserInstitutionRepository
    {
        public UserInstitutionRepository(NinaSpeakContext context) : base(context) { }

        public async Task<IEnumerable<UserInstitution>> GetByOwnerAsync(long userFk)
        {
            if (userFk <= default(long))
                return Enumerable.Empty<UserInstitution>();

            var userInstitutions = await base.GetAsync(ui => ui.UserFk == userFk && ui.Owner == true, "Institution");
            return userInstitutions.OrderBy(ui => ui.CreatedAt);
        }

        public async Task<IEnumerable<UserInstitution>> GetByUserFkAsync(long userFk, bool onlyWriter = true)
        {
            if (userFk <= default(long))
                return Enumerable.Empty<UserInstitution>();

            return await Task.FromResult(Model.Where(ui => ui.UserFk == userFk && ui.Writer == onlyWriter));
        }
    }
}
