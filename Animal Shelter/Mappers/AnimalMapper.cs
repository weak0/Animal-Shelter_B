using Animal_Shelter.Entities;
using Animal_Shelter.Models;
using AutoMapper;

namespace Animal_Shelter.Mappers;

public class AnimalMapper : Profile
{
    public AnimalMapper()
    {
        CreateMap<Animals, UpdateAnimalDto>()
            .ForMember(dest => dest.AnimalName, opt => opt.MapFrom(src => src.AnimalName))
            .ForMember(dest => dest.TypeId, opt => opt.MapFrom(src => src.TypeId))
            .ForMember(dest => dest.SizeId, opt => opt.MapFrom(src => src.SizeId));
        CreateMap<UpdateAnimalDto, Animals>().ReverseMap();
        
        CreateMap<Animals, Animal>()
            .ForMember(dest => dest.AnimalId, opt => opt.MapFrom(src => src.AnimalId))
            .ForMember(dest => dest.AnimalName, opt => opt.MapFrom(src => src.AnimalName))
            .ForMember(dest => dest.TypeId, opt => opt.MapFrom(src => src.TypeId))
            .ForMember(dest => dest.SizeId, opt => opt.MapFrom(src => src.SizeId))
            .ForMember(dest => dest.DateAdded, opt => opt.MapFrom(src => src.DateAdded))
            .ForMember(dest => dest.ShelterId, opt => opt.MapFrom(src => src.ShelterId));
        CreateMap<Animals, Animal>().ReverseMap();
        
        CreateMap<Animals, Shelter>()
            .ForMember(src => src.ShelterId, opt => opt.MapFrom(dest => dest.ShelterId));
        CreateMap<Animals, Shelter>().ReverseMap();
        
        CreateMap<Animals, AddAnimalDto>()
            .ForMember(dest => dest.AnimalName, opt => opt.MapFrom(src => src.AnimalName))
            .ForMember(dest => dest.TypeId, opt => opt.MapFrom(src => src.TypeId))
            .ForMember(dest => dest.SizeId, opt => opt.MapFrom(src => src.SizeId))
            .ForMember(dest => dest.ShelterId, opt => opt.MapFrom(src => src.ShelterId));
        CreateMap<Animals, AddAnimalDto>().ReverseMap();
    }
    
}