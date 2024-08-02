using AutoMapper;
using NinaSpeakV2.Data.Entities;
using NinaSpeakV2.Domain.ViewModels.Institutions;

namespace NinaSpeakV2.Domain.Profiles
{
    public class InstitutionProfile : Profile
    {
        public InstitutionProfile()
        {
            CreateMap<CreateInstitutionViewModel, Institution>()
                .ForMember(mbm => mbm.Image, opt => opt.MapFrom(src => src.FileName));

            CreateMap<UpdateInstitutionViewModel, Institution>()
                .ForMember(mbm => mbm.Image, opt => opt.MapFrom(src => src.FileName));

            CreateMap<ReadInstitutionViewModel, UpdateInstitutionViewModel>();
            CreateMap<Institution, ReadInstitutionViewModel>();
        }
    }
}
