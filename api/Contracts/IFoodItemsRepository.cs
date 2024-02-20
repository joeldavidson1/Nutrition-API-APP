using Entities.Models;
using Shared.RequestFeatures;

namespace Contracts;

public interface IFoodItemsRepository
{
   Task<PagedList<FoodItems>> GetAllFoodItemsAsync(FoodItemParameters  foodItemParameters, bool trackChanges);
    Task<FoodItems> GetFoodItemAsync(string foodCode, bool trackChanges);
    Task<PagedList<FoodItems>> GetFoodItemsForFoodGroupAsync(FoodItemParameters foodItemParameters,
        string foodGroupCode, bool trackChanges);
}