using Microsoft.EntityFrameworkCore;
using NinaSpeakV2.Data.Entities;
using NinaSpeakV2.Data.Repositories.IRepositories;

namespace NinaSpeakV2.Data.Repositories
{
    public class InstitutionRepository : BaseRepository<Institution>, IInstitutionRepository
    {
        public InstitutionRepository(NinaSpeakContext context) : base(context) { }
        
        public async Task<Institution> GetStandardAsync()
        {
            return (await GetByAsync(i => i.Name == Institution.StandardName))!;
        }

        public async Task<bool> NameAlreadyExistAsync(string name)
        {
            if (string.IsNullOrEmpty(name) || string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Invalid Name");

            return await Entity.AnyAsync(i => i.Name == name);
        }

        public async Task<bool> CanChangeNameAsync(Institution institution, string newName)
        {
            ArgumentNullException.ThrowIfNull(institution, nameof(institution));

            if (string.IsNullOrEmpty(newName) || string.IsNullOrWhiteSpace(newName))
                throw new ArgumentException("Invalid Name");

            var nameOwner = await GetByAsync(i => i.Name == newName);
            return nameOwner is null || nameOwner.Id == institution.Id;
        }        

        public async Task UpdateCodeAsync(long id)
        {
            var newCode = Guid.NewGuid().ToString()[..8];
            await Entity.Where(i => i.Id == id && i.Name != Institution.StandardName).ExecuteUpdateAsync(s => s.SetProperty(i => i.Code, newCode));
        }
    }
}
