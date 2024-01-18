using NutritionAPI.Dto;
using NutritionAPI.Models;

namespace NutritionAPI.Interfaces;

public interface IMappingService
{
    IEnumerable<FoodItemDto> MapFoodItemsToDtos(IEnumerable<FoodItems> foodItems);
    FoodItemDto MapFoodItemToDto(FoodItems foodItems);
    FoodGroupDto MapFoodGroupToDto(FoodGroup foodGroups);
}