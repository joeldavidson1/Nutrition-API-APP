using NutritionAPI.Models;

namespace NutritionAPI.Interfaces;

public interface IFoodItemRepository
{
    Task<IEnumerable<FoodItems>> GetFoodItems();
    Task<FoodItems> GetFoodItem(string foodCode);
    bool FoodItemExists(string foodCode);
    // Task<IEnumerable<FoodItem>> SearchFoodItemsByName(string searchTerm);
}