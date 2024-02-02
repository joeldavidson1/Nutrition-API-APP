using Contracts;
using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Repository.Extensions;
using Shared.RequestFeatures;

namespace Repository;

public class FoodItemsRepository : RepositoryBase<FoodItems>, IFoodItemsRepository
{
    public FoodItemsRepository(RepositoryContext repositoryContext) : base(repositoryContext)
    {
    }

    public async Task<PagedList<FoodItems>> GetAllFoodItemsAsync(FoodItemParameters foodItemParameters,
            bool trackChanges)
    {
        List<FoodItems> foodItems = await FindAll(trackChanges)
            .Include(fi => fi.FoodGroup)
            .Include(fi => fi.Energy)
            .Include(fi => fi.Macronutrients)
            .Include(fi => fi.Proximates)
            .Include(fi => fi.Vitamins)
            .Include(fi => fi.Minerals)
            .Search(foodItemParameters.SearchFoodByName)
            .Sort(foodItemParameters.OrderBy)
            .ToListAsync();

        return PagedList<FoodItems>
            .ToPagedList(foodItems, foodItemParameters.PageNumber, foodItemParameters.PageSize);
    }

    public async Task<FoodItems> GetFoodItemAsync(string foodCode, bool trackChanges) =>
        await FindByCondition(x => x.FoodCode.Equals(foodCode), trackChanges)
            .Include(fi => fi.Energy)
            .Include(fi => fi.Macronutrients)
            .Include(fi => fi.Proximates)
            .Include(fi => fi.Vitamins)
            .Include(fi => fi.Minerals)
            .SingleOrDefaultAsync();

    public async Task<PagedList<FoodItems>> GetFoodItemsForFoodGroupAsync(FoodItemParameters foodItemParameters,
        string foodGroupCode, bool trackChanges)
    {
        List<FoodItems> foodItems = await FindByCondition(x => x.FoodGroupCode.Equals(foodGroupCode.ToUpper()), trackChanges)
            .Include(fi => fi.Energy)
            .Include(fi => fi.Macronutrients)
            .Include(fi => fi.Proximates)
            .Include(fi => fi.Vitamins)
            .Include(fi => fi.Minerals)
            .Search(foodItemParameters.SearchFoodByName)
            .Sort(foodItemParameters.OrderBy)
            .ToListAsync();

        return PagedList<FoodItems>
            .ToPagedList(foodItems, foodItemParameters.PageNumber, foodItemParameters.PageSize);
    }
}