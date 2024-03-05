namespace Shared.DataTransferObjects;

public record class VitaminsDto(double? Retinol_mcg, double? Thiamin_mg, double? Riboflavin_mg, double? Niacin_mg,
    double? Pantothenate_mg, double? Biotin_mcg, double? Folate_mcg, double? VitaminB12_mcg, double? VitaminB6_mg, double? VitaminC_mg,
    double? VitaminD_mcg, double? VitaminE_mg, double? VitaminK1_mcg, double? Carotene_mcg, double? Tryptophan_mg);

