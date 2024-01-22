using Entities.Models;
using Shared.DataTransferObjects;
using Shared.RequestFeatures;

namespace Service.Contracts;

public interface IFoodItemsService
{
    Task<(IEnumerable<FoodItemsDto> foodItems, MetaData metaData)> GetAllFoodItemsAsync(FoodItemParameters foodItemParameters,
        bool trackChanges);
    Task<FoodItemsDto> GetFoodItemAsync(string foodCode, bool trackChanges);
    Task<IEnumerable<FoodItemsDto>> GetFoodItemsForFoodGroupAsync(string foodGroupCode, bool trackChanges);
}