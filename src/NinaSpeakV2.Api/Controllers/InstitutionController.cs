using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using NinaSpeakV2.Api.Extensions;
using NinaSpeakV2.Data.Models;
using NinaSpeakV2.Domain.Entities;
using NinaSpeakV2.Domain.Services.IServices;
using NinaSpeakV2.Domain.Validators;
using NinaSpeakV2.Domain.ViewModels.Institutions;

namespace NinaSpeakV2.Api.Controllers
{
    public class InstitutionController
        : BaseController<Institution, CreateInstitutionViewModel, UpdateInstitutionViewModel, ReadInstitutionViewModel>
    {
        public InstitutionController(IInstitutionService institutionService, IMapper mapper) 
            : base(institutionService, mapper)
        {
        }

        public override async Task<IActionResult> Create(CreateInstitutionViewModel createModel)
        {
            if (!BaseValidator.IsValid(createModel))
            {
                ViewData = ViewData.SetBaseErrors([new BaseError(BaseError.NullObject)]);
                return View();
            }

            createModel.UserFk = User.GetCurrentUserId();
            return await base.Create(createModel);
        }
    }
}
