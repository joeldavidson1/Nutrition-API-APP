namespace Shared.DataTransferObjects;

public record FoodItemsDto(string? FoodCode, string? Name, string? Description, string? DataReferences,
    EnergyDto? Energy = null, MacronutrientsDto? Macronutrients = null, ProximatesDto Proximates = null,
    VitaminsDto? Vitamins = null, MineralsDto? Minerals = null);