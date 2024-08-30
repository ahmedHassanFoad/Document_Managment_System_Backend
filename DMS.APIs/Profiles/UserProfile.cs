using AutoMapper;
using DMS.APIs.Dto;
using DMS.Core.Entities;
using DMS.Core.Entities.Identity;

namespace DMS.APIs.Profiles
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<AppUser, UserDto>()
                .ForMember(dest => dest.workSpaceID, opt => opt.MapFrom(src => src.WorkSpaceID)); // Ensure this maps correctly
      
            CreateMap<WorkSpace, WorSpaceDto>();
        }
    }

       
}
