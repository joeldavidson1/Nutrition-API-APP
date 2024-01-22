namespace Shared.DataTransferObjects;

public record FoodItemsDto(string? FoodCode, string? Name, string? Description, string? DataReferences,
    MacronutrientsAndEnergyDto MacronutrientsAndEnergy);