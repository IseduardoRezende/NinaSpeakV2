using AutoMapper;
using NinaSpeakV2.Data.Entities;
using NinaSpeakV2.Domain.Services.IServices;
using NinaSpeakV2.Domain.ViewModels.Users;

namespace NinaSpeakV2.Api.Controllers
{
    public class UserController : BaseController<User, CreateUserViewModel, UpdateUserViewModel, ReadUserViewModel>
    {
        public UserController(IUserService userService, IMapper mapper) : base(userService, mapper)
        { }       
    }
}
