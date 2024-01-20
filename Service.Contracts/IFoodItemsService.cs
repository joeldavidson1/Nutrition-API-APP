using Entities.Models;
using Shared.DataTransferObjects;

namespace Service.Contracts;

public interface IFoodItemsService
{
    Task<IEnumerable<FoodItemsDto>> GetAllFoodItemsAsync(bool trackChanges);
    Task<FoodItemsDto> GetFoodItemAsync(string foodCode, bool trackChanges);
    Task<IEnumerable<FoodItemsDto>> GetFoodItemsForFoodGroupAsync(string foodGroupCode, bool trackChanges);
}