using Animal_Shelter.Entities;
using Animal_Shelter.Models;
using AutoMapper;

namespace Animal_Shelter.Mappers;

public class CostMapper : Profile
{
    public CostMapper()
    {
        
    CreateMap<AddCostDto, Costs>()
    .ForMember(dest => dest.CostName, opt => opt.MapFrom(src => src.CostName))
    .ForMember(dest => dest.CostDescription, opt => opt.MapFrom(src => src.CostDescription))
    .ForMember(dest => dest.CategoryId, opt => opt.MapFrom(src => src.CategoryId))
    .ForMember(dest => dest.ShelterId, opt => opt.MapFrom(src => src.ShelterId))
    .ForMember(dest => dest.Cost, opt => opt.MapFrom(src => src.Cost))
    .ForMember(dest => dest.PaymentPeriodId, opt => opt.MapFrom(src => src.PaymentPeriodId));
    CreateMap<AddCostDto, Costs>().ReverseMap();
    
    CreateMap<Costs, GetCostsDto>()
    .ForMember(dest => dest.CostId, opt => opt.MapFrom(src => src.CostId))
    .ForMember(dest => dest.CostName, opt => opt.MapFrom(src => src.CostName))
    .ForMember(dest => dest.CostDescription, opt => opt.MapFrom(src => src.CostDescription))
    .ForMember(dest => dest.CategoryId, opt => opt.MapFrom(src => src.CategoryId))
    .ForMember(dest => dest.Cost, opt => opt.MapFrom(src => src.Cost));
    CreateMap<Costs, GetCostsDto>().ReverseMap();

    CreateMap<Costs, UpdateCostDto>()
    .ForMember(dest => dest.CostId, opt => opt.MapFrom(src => src.CostId))
    .ForMember(dest => dest.CostName, opt => opt.MapFrom(src => src.CostName))
    .ForMember(dest => dest.CostDescription, opt => opt.MapFrom(src => src.CostDescription))
    .ForMember(dest => dest.CategoryId, opt => opt.MapFrom(src => src.CategoryId))
    .ForMember(dest => dest.Cost, opt => opt.MapFrom(src => src.Cost));
    CreateMap<Costs, UpdateCostDto>().ReverseMap();
    }
}