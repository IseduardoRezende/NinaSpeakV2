using AutoMapper;
using NinaSpeakV2.Data.Entities;
using NinaSpeakV2.Domain.ViewModels.Users;

namespace NinaSpeakV2.Domain.Profiles
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<CreateUserViewModel, User>();
            CreateMap<UpdateUserViewModel, User>();
            CreateMap<ReadUserViewModel, UpdateUserViewModel>();
            CreateMap<User, ReadUserViewModel>();
        }
    }
}
