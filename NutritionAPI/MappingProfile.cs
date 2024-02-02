using System.Reflection;
using AutoMapper;
using Entities.Models;
using Shared;
using Shared.DataTransferObjects;

namespace NutritionAPI;

public class MappingProfile : Profile
{ 
    public MappingProfile()
    {
        CreateMap<FoodItems, FoodItemsDto>();
        CreateMap<FoodGroups, FoodGroupsDto>();
        CreateMap<Energy, EnergyDto>();
        CreateMap<Proximates, ProximatesDto>();
        CreateMap<Macronutrients, MacronutrientsDto>();
        CreateMap<Vitamins, VitaminsDto>();
        CreateMap<Minerals, MineralsDto>();
    }
}
