using Shared.DataTransferObjects;

namespace Shared.DataTransferObjects;

public record ProximatesDto(double? Water_g, double? Starch_g, double? TotalSugars_g, double? Glucose_g, double? Galactose_g,
    double? Fructose_g, double? Sucrose_g, double? Maltose_g, double? Lactose_g, double? Alcohol_g, double? NonStarchPolysaccharides_g,
    double? Fibre_g, double? FatsSaturated_g, double? FatsMonounsaturated_g, double? FatsPolyunsaturated_g, 
    double? FatsTrans_g, double? Cholesterol_g);


