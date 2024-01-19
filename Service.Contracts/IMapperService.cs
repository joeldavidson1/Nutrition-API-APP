using Entities.Models;
using Shared.DataTransferObjects;

namespace Service.Contracts;

public interface IMapperService
{
    IEnumerable<FoodItemsDto> MapFoodItemsToDtos(IEnumerable<FoodItems> foodItems);
    FoodItemsDto MapFoodItemToDto(FoodItems foodItem);
}