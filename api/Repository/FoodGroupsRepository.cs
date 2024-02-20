using Contracts;
using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Repository.Extensions;
using Shared.RequestFeatures;

namespace Repository;

public class FoodGroupsRepository : RepositoryBase<FoodGroups>, IFoodGroupsRepository
{
    public FoodGroupsRepository(RepositoryContext repositoryContext) : base(repositoryContext)
    {
        
    }
    
    public async Task<IEnumerable<FoodGroups>> GetAllFoodGroupsAsync(FoodGroupParameters foodGroupParameters, 
        bool trackChanges) =>
        await FindAll(trackChanges)
            .Search(foodGroupParameters.SearchFoodGroupByDescription)
            .OrderBy(x => x.FoodGroupCode)
            .ToListAsync();

    public async Task<FoodGroups> GetFoodGroupAsync(string foodGroupCode, bool trackChanges) =>
        await FindByCondition(x => x.FoodGroupCode.Equals(foodGroupCode.ToUpper()), trackChanges)
            .SingleOrDefaultAsync();
}