using NutritionAPI.Dto;
using NutritionAPI.Models;

namespace NutritionAPI.Interfaces;

public interface IMappingService
{
    IEnumerable<FoodItemDto> MapFoodItemsToDtos(IEnumerable<FoodItem> foodItems);
}