using Entities.Models;
using Shared.DataTransferObjects;

namespace Service.Contracts;

public interface IMapperService
{
    IEnumerable<FoodItemsDto> MapFoodItemsToDto(IEnumerable<FoodItems> foodItems);
    FoodItemsDto MapFoodItemToDto(FoodItems foodItem);
    IEnumerable<FoodGroupsDto> MapFoodGroupsToDto(IEnumerable<FoodGroups> foodItems);
    FoodGroupsDto MapFoodGroupToDto(FoodGroups foodGroup);
}