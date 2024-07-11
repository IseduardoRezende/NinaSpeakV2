using NinaSpeakV2.Data.Models;
using NinaSpeakV2.Data.Repositories.IRepositories;

namespace NinaSpeakV2.Data.Repositories
{
    public class UserInstitutionRepository : BaseRepository<UserInstitution>, IUserInstitutionRepository
    {
        public UserInstitutionRepository(NinaSpeakContext context) : base(context) { }

        public async Task<IEnumerable<UserInstitution>> UpdateAsync(IEnumerable<UserInstitution> userInstitutions)
        {
            ArgumentNullException.ThrowIfNull(userInstitutions, nameof(userInstitutions));

            if (!userInstitutions.Any())
                throw new Exception();

            Model.UpdateRange(userInstitutions);

            if (!await SaveChangesAsync())
                throw new Exception();

            return await GetMembersByInstitutionFkAsync(userInstitutions.First().InstitutionFk);
        }

        public override async Task<bool> SoftDeleteAsync(UserInstitution model)
        {
            if (model is null)
                return false;

            model.DeletedAt = DateTime.Now;

            if (model.Creator)
                model.Creator = false;

            Model.Update(model);
            return await SaveChangesAsync();
        }

        public async Task<bool> SoftDeleteAsync(IEnumerable<UserInstitution> userInstitutions)
        {
            ArgumentNullException.ThrowIfNull(userInstitutions, nameof(userInstitutions));

            if (!userInstitutions.Any())
                throw new Exception();

            foreach (var userInstitution in userInstitutions)
                userInstitution.DeletedAt = DateTime.Now;

            Model.UpdateRange(userInstitutions);
            return await SaveChangesAsync();
        }

        public async Task<IEnumerable<UserInstitution>> GetMembersByInstitutionFkAsync(long institutionFk)
        {
            if (institutionFk <= default(long))
                return Enumerable.Empty<UserInstitution>();

            var usersInstitution = await base.GetAsync(ui => ui.InstitutionFk == institutionFk, "User", "Institution");
            return usersInstitution.OrderBy(ui => ui.User.Email);
        }

        public async Task<IEnumerable<UserInstitution>> GetByOwnerAsync(long userFk)
        {
            if (userFk <= default(long))
                return Enumerable.Empty<UserInstitution>();

            return await base.GetAsync(ui => ui.UserFk == userFk && ui.Owner, "User", "Institution");
        }

        public async Task<IEnumerable<UserInstitution>> GetByUserFkAsync(long userFk, bool onlyWriter = false)
        {
            if (userFk <= default(long))
                return Enumerable.Empty<UserInstitution>();

            if (onlyWriter)
            {
                return await base.GetAsync(ui => ui.UserFk == userFk && ui.Writer, "User", "Institution");
            }

            return await base.GetAsync(ui => ui.UserFk == userFk, "User", "Institution");
        }
    }
}
