using NutritionAPI.Data;
using NutritionAPI.Interfaces;
using NutritionAPI.Models;

namespace NutritionAPI.Repository;

public class FoodItemRepository : IFoodItemRepository
{
    private readonly DataContext _context;

    public FoodItemRepository(DataContext context)
    {
        _context = context;
    }

    public ICollection<FoodItem> GetFoodItems()
    {
        return _context.FoodItem.ToList();
    }

    public FoodItem GetFoodItem(string foodCode)
    {
        return _context.FoodItem.FirstOrDefault(foodItem => foodItem.FoodCode == foodCode);
    }

    public bool FoodItemExists(string foodCode)
    {
        return _context.FoodItem.Any(foodItem => foodItem.FoodCode == foodCode);
    }
}