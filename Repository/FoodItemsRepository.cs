using Contracts;
using Entities.Models;
using Microsoft.EntityFrameworkCore;
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
            .Include(f => f.NutrientValues)
            .ThenInclude(nv => nv.NutrientsAndEnergy)
            .ThenInclude(n => n.NutrientCategories)
            .OrderBy(x => x.Name)
            .ToListAsync();

        return PagedList<FoodItems>
            .ToPagedList(foodItems, foodItemParameters.PageNumber, foodItemParameters.PageSize);
    }

    public async Task<FoodItems> GetFoodItemAsync(string foodCode, bool trackChanges) =>
        await FindByCondition(x => x.FoodCode.Equals(foodCode), trackChanges)
            .Include(f => f.NutrientValues)
            .ThenInclude(nv => nv.NutrientsAndEnergy)
            .ThenInclude(n => n.NutrientCategories)
            .SingleOrDefaultAsync();

    public async Task<IEnumerable<FoodItems>> GetFoodItemsForFoodGroupAsync(string foodGroupCode, bool trackChanges) => 
        await FindByCondition(x => x.FoodGroupCode.Equals(foodGroupCode.ToUpper()), trackChanges)
            .OrderBy(c => c.Name)
            .ToListAsync();
}