using NutritionAPI.Dto;
using NutritionAPI.Interfaces;
using NutritionAPI.Models;

namespace NutritionAPI.Helper;

public class MappingService : IMappingService
{
    public IEnumerable<FoodItemDto> MapFoodItemsToDtos(IEnumerable<FoodItems> foodItems)
    {
        return foodItems.Select(foodItem => new FoodItemDto
        {
            FoodCode = foodItem.FoodCode,
            Name = foodItem.Name,
            // FoodGroupCode = foodItem.FoodGroupCode,
            FoodGroup = MapFoodGroupToDto(foodItem.FoodGroup)
        });
    }

    public FoodItemDto MapFoodItemToDto(FoodItems foodItems)
    {
        return new FoodItemDto
        {
            FoodCode = foodItems.FoodCode,
            Name = foodItems.Name,
            FoodGroup = MapFoodGroupToDto(foodItems.FoodGroup)
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
}