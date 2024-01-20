using Entities.Models;

namespace Contracts;

public interface IFoodItemsRepository
{
   Task<IEnumerable<FoodItems>> GetAllFoodItemsAsync(bool trackChanges);
    Task<FoodItems> GetFoodItemAsync(string foodCode, bool trackChanges);
    Task<IEnumerable<FoodItems>> GetFoodItemsForFoodGroupAsync(string foodGroupCode, bool trackChanges);
}