using AutoMapper;
using Entities.Models;
using Shared.DataTransferObjects;

namespace NutritionAPI;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<MacronutrientsAndEnergy, MacronutrientsAndEnergyDto>();
        
        CreateMap<FoodItems, FoodItemsDto>()
            .ForMember(dto => dto.MacronutrientsAndEnergy, opt => opt.MapFrom(src => src.MacronutrientsAndEnergy));
    }
}