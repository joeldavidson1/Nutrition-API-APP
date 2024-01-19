using Entities.Models;

namespace Contracts;

public interface IFoodItemsRepository
{
    IEnumerable<FoodItems> GetAllFoodItems(bool trackChanges);
    FoodItems GetFoodItem(string FoodCode, bool trackChanges);
}