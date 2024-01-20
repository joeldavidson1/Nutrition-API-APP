using Entities.Models;
using Service.Contracts;
using Shared.DataTransferObjects;

namespace Service;

public class MapperService : IMapperService
{
    public IEnumerable<FoodItemsDto> MapFoodItemsToDto(IEnumerable<FoodItems> foodItems)
    {
        return foodItems.Select(foodItem => new FoodItemsDto
        (
            foodItem.FoodCode,
            foodItem.Name,
            foodItem.Description,
            foodItem.DataReferences,
            MacronutrientsAndEnergyDto(foodItem.MacronutrientsAndEnergy)
        ));
    }
    
    public FoodItemsDto MapFoodItemToDto(FoodItems foodItem)
    {
        return new FoodItemsDto(
            foodItem.FoodCode,
            foodItem.Name, 
            foodItem.Description,
            foodItem.DataReferences,
            MacronutrientsAndEnergyDto(foodItem.MacronutrientsAndEnergy)
            );
    }

    public IEnumerable<FoodGroupsDto> MapFoodGroupsToDto(IEnumerable<FoodGroups> foodGroups)
    {
        return foodGroups.Select(foodGroup => new FoodGroupsDto
        (
            foodGroup.FoodGroupCode,
            foodGroup.Description
        ));
    }

    public FoodGroupsDto MapFoodGroupToDto(FoodGroups foodGroup)
    {
        return new FoodGroupsDto(foodGroup.FoodGroupCode, foodGroup.Description);
    }

    public MacronutrientsAndEnergyDto MacronutrientsAndEnergyDto(MacronutrientsAndEnergy macronutrientsAndEnergy)
    {
        return new MacronutrientsAndEnergyDto(
            macronutrientsAndEnergy.EnergyKcal,
            macronutrientsAndEnergy.EnergyKj,
            macronutrientsAndEnergy.Protein, 
            macronutrientsAndEnergy.Fat,
            macronutrientsAndEnergy.Carbohydrate);
    }
}