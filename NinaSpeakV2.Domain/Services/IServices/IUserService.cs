using NinaSpeakV2.Data.Entities;
using NinaSpeakV2.Domain.ViewModels.Users;

namespace NinaSpeakV2.Domain.Services.IServices
{
    public interface IUserService : IBaseService<User, CreateUserViewModel, UpdateUserViewModel, ReadUserViewModel>
    {
    }
}
