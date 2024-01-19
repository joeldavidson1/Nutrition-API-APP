using Entities.Models;
using Shared.DataTransferObjects;

namespace Service.Contracts;

public interface IFoodItemsService
{
    IEnumerable<FoodItemsDto> GetAllFoodItems(bool trackChanges);
    FoodItemsDto GetFoodItem(string foodCode, bool trackChanges);
}