using Contracts;
using Entities.Models;

namespace Repository;

public class FoodGroupsRepository : RepositoryBase<FoodGroups>, IFoodGroupsRepository
{
    public FoodGroupsRepository(RepositoryContext repositoryContext) : base(repositoryContext)
    {
        
    }
    
    public IEnumerable<FoodGroups> GetAllFoodGroups(bool trackChanges) =>
        FindAll(trackChanges)
            .OrderBy(x => x.FoodGroupCode)
            .ToList();

    public FoodGroups GetFoodGroup(string foodGroupCode, bool trackChanges) =>
        FindByCondition(x => x.FoodGroupCode.Equals(foodGroupCode.ToUpper()), trackChanges)
            .SingleOrDefault();
}