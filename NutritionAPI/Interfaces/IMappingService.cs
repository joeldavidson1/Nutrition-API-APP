using NutritionAPI.Dto;
using NutritionAPI.Models;

namespace NutritionAPI.Interfaces;

public interface IMappingService
{
    IEnumerable<FoodItemsDto> MapFoodItemsToDtos(IEnumerable<FoodItems> foodItems);
    FoodItemsDto MapFoodItemToDto(FoodItems foodItems);
    FoodGroupDto MapFoodGroupToDto(FoodGroup foodGroups);
    ProximatesDto MapProximatesToDto(Proximates proximates);
}