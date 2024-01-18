using NutritionAPI.Models;

namespace NutritionAPI.Interfaces;

public interface IFoodItemRepository
{
    Task<int> GetTotalFoodItemsCount();
    Task<IEnumerable<FoodItems>> GetAllFoodItemsAsync(FoodItemsParameters foodItemsParameters, bool trackChanges);
    // IQueryable<FoodItems> GetFoodItems(FoodItemsParameters);
    Task<FoodItems> GetFoodItemByFoodCode(string foodCode);
    bool FoodItemExists(string foodCode);
    // Task<IEnumerable<FoodItem>> SearchFoodItemsByName(string searchTerm);
}