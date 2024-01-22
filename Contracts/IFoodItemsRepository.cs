using Entities.Models;
using Shared.RequestFeatures;

namespace Contracts;

public interface IFoodItemsRepository
{
   Task<PagedList<FoodItems>> GetAllFoodItemsAsync(FoodItemParameters  foodItemParameters, bool trackChanges);
    Task<FoodItems> GetFoodItemAsync(string foodCode, bool trackChanges);
    Task<IEnumerable<FoodItems>> GetFoodItemsForFoodGroupAsync(string foodGroupCode, bool trackChanges);
}