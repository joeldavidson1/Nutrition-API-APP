using Entities.Models;

namespace Contracts;

public interface IFoodItemsRepository
{
    IEnumerable<FoodItems> GetAllFoodItems(bool trackChanges);
    FoodItems GetFoodItem(string foodCode, bool trackChanges);
    IEnumerable<FoodItems> GetFoodItemsForFoodGroup(string foodGroupCode, bool trackChanges);
}