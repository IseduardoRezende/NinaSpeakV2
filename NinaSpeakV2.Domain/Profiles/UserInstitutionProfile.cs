using AutoMapper;
using NinaSpeakV2.Data.Entities;
using NinaSpeakV2.Domain.ViewModels.UsersInstitutions;

namespace NinaSpeakV2.Domain.Profiles
{
    public class UserInstitutionProfile : Profile
    {
        public UserInstitutionProfile()
        {
            CreateMap<CreateUserInstitutionViewModel, UserInstitution>();
            CreateMap<UpdateUserInstitutionViewModel, UserInstitution>();
            CreateMap<ReadUserInstitutionViewModel, UpdateUserInstitutionViewModel>();
            CreateMap<UserInstitution, ReadUserInstitutionViewModel>();
            CreateMap<ReadUserInstitutionViewModel, UserInstitution>();
        }
    }
}
