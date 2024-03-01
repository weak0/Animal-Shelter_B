using Animal_Shelter.Entities;
using Animal_Shelter.Models;
using AutoMapper;

namespace Animal_Shelter.Mappers;

public class AnimalShelterMappingProfile : Profile
{
    public AnimalShelterMappingProfile()
    {
        CreateMap<Animals, GetAnimalDto>()
            .ForMember(dest => dest.AnimalId, opt => opt.MapFrom(src => src.AnimalId))
            .ForMember(dest => dest.AnimalName, opt => opt.MapFrom(src => src.AnimalName))
            .ForMember(dest => dest.Type, opt => opt.MapFrom(src => src.Type))
            .ForMember(dest => dest.Size, opt => opt.MapFrom(src => src.Size))
            .ForMember(dest => dest.DateAdded, opt => opt.MapFrom(src => src.DateAdded))
            .ForMember(dest => dest.ShelterId, opt => opt.MapFrom(src => src.ShelterId));
        CreateMap<Animals, GetAnimalDto>().ReverseMap();
        
        CreateMap<Animals, Shelter>()
            .ForMember(src => src.ShelterId, opt => opt.MapFrom(dest => dest.ShelterId));
        CreateMap<Animals, Shelter>().ReverseMap();
        
        CreateMap<Animals, AddAnimalDto>()
            .ForMember(dest => dest.AnimalName, opt => opt.MapFrom(src => src.AnimalName))
            .ForMember(dest => dest.Type, opt => opt.MapFrom(src => src.Type))
            .ForMember(dest => dest.Size, opt => opt.MapFrom(src => src.Size))
            .ForMember(dest => dest.ShelterId, opt => opt.MapFrom(src => src.ShelterId));
        CreateMap<Animals, AddAnimalDto>().ReverseMap();
        
    }
    
}