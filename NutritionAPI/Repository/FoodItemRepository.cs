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
        return _context.FoodItems.ToList();
    }
}