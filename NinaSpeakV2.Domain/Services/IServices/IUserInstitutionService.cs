﻿using NinaSpeakV2.Data.Entities;
using NinaSpeakV2.Domain.ViewModels.UsersInstitutions;

namespace NinaSpeakV2.Domain.Services.IServices
{
    public interface IUserInstitutionService 
        : IBaseService<UserInstitution, CreateUserInstitutionViewModel, UpdateUserInstitutionViewModel, ReadUserInstitutionViewModel>
    {
        Task<IEnumerable<ReadUserInstitutionViewModel>> UpdateAsync(IEnumerable<UpdateUserInstitutionViewModel> updateViewModels);

        Task<bool> SoftDeleteByInstitutionFkAsync(long institutionFk);

        Task<bool> SoftDeleteAsync(long userFk, long institutionFk);

        Task<ReadUserInstitutionViewModel> StandardRegistrationAsync(long userFk);

        Task<IEnumerable<ReadUserInstitutionViewModel>> GetByOwnerAsync(long userFk);

        Task<IEnumerable<ReadUserInstitutionViewModel>> GetByUserFkAsync(long userFk, bool onlyWriter = false);

        Task<IEnumerable<ReadUserInstitutionViewModel>> GetMembersByInstitutionFkAsync(long institutionFk);
    }
}
