using Entities.Models;
using Service.Contracts;
using Shared.DataTransferObjects;

namespace Service;

public class MapperService : IMapperService
{
    public IEnumerable<FoodItemsDto> MapFoodItemsToDtos(IEnumerable<FoodItems> foodItems)
    {
        return foodItems.Select(foodItem => new FoodItemsDto
        (
            foodItem.FoodCode,
            foodItem.Name,
            foodItem.Description,
            foodItem.DataReferences
        ));
    }
    
    public FoodItemsDto MapFoodItemToDto(FoodItems foodItem)
    {
        return new FoodItemsDto(foodItem.FoodCode, foodItem.Name, foodItem.Description, foodItem.DataReferences);
    }
}