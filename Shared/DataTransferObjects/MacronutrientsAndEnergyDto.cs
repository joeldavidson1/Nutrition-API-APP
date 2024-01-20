namespace Shared.DataTransferObjects;

public record MacronutrientsAndEnergyDto(string? EnergyKcal, string? EnergyKj, string? Protein,
    string? Fat, string? Carbohydrate);