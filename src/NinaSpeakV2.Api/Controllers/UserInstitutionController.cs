using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using NinaSpeakV2.Data.Models;
using NinaSpeakV2.Domain.Services.IServices;
using NinaSpeakV2.Domain.ViewModels.UsersInstitutions;

namespace NinaSpeakV2.Api.Controllers
{
    public class UserInstitutionController
        : BaseController<UserInstitution, CreateUserInstitutionViewModel, UpdateUserInstitutionViewModel, ReadUserInstitutionViewModel>
    {
        public UserInstitutionController(IUserInstitutionService userInstitutionService, IMapper mapper) 
            : base(userInstitutionService, mapper)
        {
        }

        public override async Task<IActionResult> Index()
        {
            return View(await _readonlyService.GetAsync("User", "Institution"));
        }
    }
}
