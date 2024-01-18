using NutritionAPI.Dto;
using NutritionAPI.Models;

namespace NutritionAPI.Services;

public interface IFoodItemsService
{
    Task<IEnumerable<FoodItemsDto>> GetAllFoodItemsAsync(FoodItemsParameters foodItemsParameters, bool trackChanges);
}