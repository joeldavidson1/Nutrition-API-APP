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

    public async Task<IEnumerable<FoodItem>> GetFoodItems()
    {
        // return await _context.FoodItem.ToListAsync();
        return await _context.FoodItem.Include(foodItem => foodItem.FoodGroup).ToListAsync();
    }

    public async Task<FoodItem> GetFoodItem(string foodCode)
    {
        return await _context.FoodItem.FirstOrDefaultAsync(foodItem => foodItem.FoodCode == foodCode);
    }

    public bool FoodItemExists(string foodCode)
    {
        return _context.FoodItem.Any(foodItem => foodItem.FoodCode == foodCode);
    }
}