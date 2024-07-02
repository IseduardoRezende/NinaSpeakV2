﻿using NinaSpeakV2.Data.Models;
using NinaSpeakV2.Domain.ViewModels.Institutions;

namespace NinaSpeakV2.Domain.Services.IServices
{
    public interface IInstitutionService 
        : IBaseService<Institution, CreateInstitutionViewModel, UpdateInstitutionViewModel, ReadInstitutionViewModel>
    {
    }
}
