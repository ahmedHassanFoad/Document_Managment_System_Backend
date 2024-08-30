using AutoMapper;
using DMS.APIs.Dto;
using DMS.Core.Entities.Identity;

namespace DMS.APIs.Profiles
{
    public class RegisterProfile : Profile
    {
        public RegisterProfile()
        {
            CreateMap<RegisterDto, AppUser>()
                //.ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.Email.Split('@')[0]))
                .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.NID, opt => opt.MapFrom(src => src.NID))
                .ForMember(dest => dest.PhoneNumber, opt => opt.MapFrom(src => src.PhoneNumber));
                //.ForMember(dest => dest.WorkSpaceID, opt => opt.MapFrom(src => src.WorkSpaceName));
        }
    }
}
