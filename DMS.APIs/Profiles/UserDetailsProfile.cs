using AutoMapper;
using DMS.APIs.Dto;
using DMS.Core.Entities;
using DMS.Core.Entities.Identity;

namespace DMS.APIs.Profiles
{
    public class UserDetailsProfile :Profile
    {
        public UserDetailsProfile()
        {
            CreateMap<AppUser, UserDetailsDto>()
                .ForMember(dest => dest.WorkSpace, opt => opt.MapFrom(src => src.WorkSpace));

            CreateMap<WorkSpace, WorSpaceDto>();
        }
    }
}
