using System.Runtime.CompilerServices;
using Microsoft.EntityFrameworkCore;
using NutritionAPI.Data;
using NutritionAPI.Interfaces;
using NutritionAPI.Models;

namespace NutritionAPI.Repository;

public class FoodItemsRepository : RepositoryBase<FoodItems>, IFoodItemRepository
{
    private readonly DataContext _context; // Remove this and migrate it

    public FoodItemsRepository(DataContext dataContext) : base(dataContext)
    {
        
    }

    public Task<int> GetTotalFoodItemsCount() => _context.FoodItems.CountAsync();
    
    public async Task<IEnumerable<FoodItems>> GetAllFoodItemsAsync(FoodItemsParameters foodItemsParameters, bool trackChanges) =>
        await FindAll(trackChanges)
            .Skip((foodItemsParameters.PageNumber - 1) * foodItemsParameters.PageSize)
            .Take(foodItemsParameters.PageSize)
            .Include(foodItem => foodItem.FoodGroup)
            .Include(foodItem => foodItem.Proximates)
            .ToListAsync();
    
    // public IQueryable<FoodItems> GetFoodItems(FoodItemsParameters foodItemsParameters)
    // {
    //     return _context.FoodItems
    //         .Include(foodItem => foodItem.FoodGroup)
    //         .Include(foodItem => foodItem.Proximates);
    // }

    // public async Task<IEnumerable<FoodItems>> GetFoodItems()
    // {
    //     return await _context.FoodItems
    //         .Include(foodItem => foodItem.FoodGroup)
    //         .Include(foodItem => foodItem.Proximates)
    //         .ToListAsync();
    // }

    public async Task<FoodItems> GetFoodItemByFoodCode(string foodCode)
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