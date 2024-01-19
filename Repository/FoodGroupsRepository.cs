using Contracts;
using Entities.Models;

namespace Repository;

public class FoodGroupsRepository : RepositoryBase<FoodGroups>, IFoodGroupsRepository
{
    public FoodGroupsRepository(RepositoryContext repositoryContext) : base(repositoryContext)
    {
        
    }
}