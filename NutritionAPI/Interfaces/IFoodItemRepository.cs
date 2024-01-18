using NutritionAPI.Models;

namespace NutritionAPI.Interfaces;

public interface IFoodItemRepository
{
    Task<IEnumerable<FoodItem>> GetFoodItems();
    Task<FoodItem> GetFoodItem(string foodCode);
    bool FoodItemExists(string foodCode);
    // Task<IEnumerable<FoodItem>> SearchFoodItemsByName(string searchTerm);
}