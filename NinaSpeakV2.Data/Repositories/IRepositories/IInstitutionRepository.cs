using NinaSpeakV2.Data.Entities;

namespace NinaSpeakV2.Data.Repositories.IRepositories
{
    public interface IInstitutionRepository : IBaseRepository<Institution>
    {
        Task<Institution> GetStandardAsync();

        Task<bool> NameAlreadyExistAsync(string name);

        Task<bool> CanChangeNameAsync(Institution institution, string newName);

        Task UpdateCodeAsync(long id);
    }
}
