using NutritionAPI.Models;

namespace NutritionAPI.Interfaces;

public interface IFoodItemRepository
{
    ICollection<FoodItem> GetFoodItems();
}