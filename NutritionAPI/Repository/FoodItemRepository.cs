using System.Runtime.CompilerServices;
using Microsoft.EntityFrameworkCore;
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

    public async Task<IEnumerable<FoodItems>> GetFoodItems()
    {
        return await _context.FoodItems
            .Include(foodItem => foodItem.FoodGroup)
            .Include(foodItem => foodItem.Proximates)
            .ToListAsync();
    }

    public async Task<FoodItems> GetFoodItem(string foodCode)
    {
        return await _context.FoodItems
            .Include(foodItem => foodItem.FoodGroup)
            .Include(foodItem => foodItem.Proximates)
            .FirstOrDefaultAsync(foodItem => foodItem.FoodCode == foodCode);
    }

    public bool FoodItemExists(string foodCode)
    {
        return _context.FoodItems.Any(foodItem => foodItem.FoodCode == foodCode);
    }
}