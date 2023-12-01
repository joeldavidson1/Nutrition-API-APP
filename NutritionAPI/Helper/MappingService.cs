using NutritionAPI.Dto;
using NutritionAPI.Interfaces;
using NutritionAPI.Models;

namespace NutritionAPI.Helper;

public class MappingService : IMappingService
{
    public IEnumerable<FoodItemDto> MapFoodItemsToDtos(IEnumerable<FoodItem> foodItems)
    {
        return foodItems.Select(foodItem => new FoodItemDto
        {
            FoodCode = foodItem.FoodCode,
            Name = foodItem.Name,
            FoodGroupCode = foodItem.FoodGroupCode,
            FoodGroup = MapFoodGroupToDto(foodItem.FoodGroup)
        });
    }

    public FoodGroupDto MapFoodGroupToDto(FoodGroup foodGroup)
    {
        return new FoodGroupDto
        {
            Description = foodGroup.Description,
            FoodGroupCode = foodGroup.FoodGroupCode
        };
    }
}