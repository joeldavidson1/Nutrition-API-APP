using Contracts;
using Entities.Models;

namespace Repository;

public class FoodItemsRepository : RepositoryBase<FoodItems>, IFoodItemsRepository
{
    public FoodItemsRepository(RepositoryContext repositoryContext) : base(repositoryContext)
    {
    }

    public IEnumerable<FoodItems> GetAllFoodItems(bool trackChanges) =>
        FindAll(trackChanges)
            .OrderBy(x => x.Name)
            .ToList();

    public FoodItems GetFoodItem(string foodCode, bool trackChanges) =>
        FindByCondition(x => x.FoodCode.Equals(foodCode), trackChanges)
            .SingleOrDefault();

    public IEnumerable<FoodItems> GetFoodItemsForFoodGroup(string foodGroupCode, bool trackChanges) =>
        FindByCondition(x => x.FoodGroupCode.Equals(foodGroupCode.ToUpper()), trackChanges)
            .OrderBy(c => c.Name)
            .ToList();

}