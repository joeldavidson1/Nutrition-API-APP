using NutritionAPI.Dto;
using NutritionAPI.Interfaces;
using NutritionAPI.Models;

namespace NutritionAPI.Helper;

public class MappingService : IMappingService
{
    public IEnumerable<FoodItemDto> MapFoodItemsToDtos(IEnumerable<FoodItem> foodItems)
    {
        return foodItems.Select(fi => new FoodItemDto
        {
            FoodCode = fi.FoodCode,
            Name = fi.Name,
            FoodGroupCode = fi.FoodGroupCode
        });
    }
}