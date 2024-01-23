using Shared.DataTransferObjects;

namespace Shared.DataTransferObjects;

public record ProximatesDto(string? Water, string? Starch, string? TotalSugars, string? Glucose, string? Galactose,
    string? Fructose, string? Sucrose, string? Maltose, string? Lactose, string? Alcohol, string? NonStarchPolysaccharides,
    string? Fibre, string? FatsSaturated, string? FatsMonounsaturated, string? FatsPolyunsaturated, 
    string? FatsTrans, string? Cholesterol);


