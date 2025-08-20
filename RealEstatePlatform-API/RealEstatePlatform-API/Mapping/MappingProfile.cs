using AutoMapper;
using RealEstatePlatform_API.DTOs;
using RealEstatePlatform_API.Models;
using System.Linq;

namespace RealEstatePlatform_API.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // Entity -> DTO
            CreateMap<Property, PropertyDTO>()
                .ForMember(dest => dest.Images,
                           opt => opt.MapFrom(src => src.Images.Select(i => i.ImageUrl)));

            // DTO -> Entity
            CreateMap<PropertyDTO, Property>()
                .ForMember(dest => dest.Images,
                           opt => opt.MapFrom(src => src.Images.Select(url => new PropertyImage { ImageUrl = url })));
        }
    }
}
