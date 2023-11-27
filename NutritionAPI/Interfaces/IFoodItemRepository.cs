using NutritionAPI.Models;

namespace NutritionAPI.Interfaces;

public interface IFoodItemRepository
{
    ICollection<FoodItem> GetFoodItems();
    FoodItem GetFoodItem(string foodCode);
    bool FoodItemExists(string foodCode);
}