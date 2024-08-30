using AutoMapper;
using DMS.APIs.Dto;

namespace DMS.APIs.Profiles
{
    public class DirectoryProfile :Profile
    {
        public DirectoryProfile()
        {

            CreateMap<Core.Entities.Directory, DirectoryDto>()
                    //.ForMember(dest => dest.IsPublic, opt => opt.MapFrom(src => src.IsPublic))
                    //.ForMember(dest => dest.IsDeleted, opt => opt.MapFrom(src => src.IsDeleted))
                    .ForMember(dest => dest.name, opt => opt.MapFrom(src => src.Name))
                    .ForMember(dest => dest.WorkSpaceId, opt => opt.MapFrom(src => src.WorkSpaceId)) ;

            CreateMap<DirectoryDto, Core.Entities.Directory>()
                //.ForMember(dest => dest.IsPublic, opt => opt.MapFrom(src => src.IsPublic))
                //.ForMember(dest => dest.IsDeleted, opt => opt.MapFrom(src => src.IsDeleted))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.name))
                 .ForMember(dest => dest.WorkSpaceId, opt => opt.MapFrom(src => src.WorkSpaceId));
        }
    }
}
