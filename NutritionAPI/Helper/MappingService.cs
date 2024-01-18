using NutritionAPI.Dto;
using NutritionAPI.Interfaces;
using NutritionAPI.Models;

namespace NutritionAPI.Helper;

public class MappingService : IMappingService
{
    public IEnumerable<FoodItemsDto> MapFoodItemsToDtos(IEnumerable<FoodItems> foodItems)
    {
        return foodItems.Select(foodItem => new FoodItemsDto
        {
            FoodCode = foodItem.FoodCode,
            Name = foodItem.Name,
            Description = foodItem.Description,
            DataReferences = foodItem.DataReferences,
            FoodGroup = MapFoodGroupToDto(foodItem.FoodGroup),
            Proximates = MapProximatesToDto(foodItem.Proximates)
        });
    }

    public FoodItemsDto MapFoodItemToDto(FoodItems foodItem)
    {
        return new FoodItemsDto
        {
            FoodCode = foodItem.FoodCode,
            Name = foodItem.Name,
            Description = foodItem.Description,
            DataReferences = foodItem.DataReferences,
            FoodGroup = MapFoodGroupToDto(foodItem.FoodGroup),
            Proximates = MapProximatesToDto(foodItem.Proximates)
        };
    }

    public FoodGroupDto MapFoodGroupToDto(FoodGroup foodGroup)
    {
        return new FoodGroupDto
        {
            FoodGroupCode = foodGroup.FoodGroupCode,
            Description = foodGroup.Description
        };
    }

    public ProximatesDto MapProximatesToDto(Proximates proximates)
    {
        return new ProximatesDto
        {
            Water = proximates.Water,
            Starch = proximates.Starch,
            TotalSugars = proximates.TotalSugars,
            Glucose = proximates.Glucose,
            Galactose = proximates.Galactose,
            Fructose = proximates.Fructose,
            Sucrose = proximates.Sucrose,
            Maltose = proximates.Maltose,
            Lactose = proximates.Lactose,
            Alochol = proximates.Alcohol,
            NonStarchPolysaccharides = proximates.NonStarchPolysaccharides,
            FibreAOAC = proximates.FibreAOAC,
            FatsSaturated = proximates.FatsSaturated,
            FatsMonounsaturated = proximates.FatsMonounsaturated,
            FatsPolyunsaturated = proximates.FatsPolyunsaturated,
            FatsTrans = proximates.FatsTrans,
            Cholesterol = proximates.Cholesterol
        };
    }
}