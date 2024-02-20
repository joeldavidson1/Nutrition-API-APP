using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities.Models;

public class Vitamins
{
    [Key]
    public string? FoodCode { get; set; }
    public double? Retinol_mcg { get; set; }
    public double? Thiamin_mg { get; set; }
    public double? Riboflavin_mg { get; set; }
    public double? Niacin_mg { get; set; }
    public double? Pantothenate_mg { get; set; }
    public double? Biotin_mcg { get; set; }
    public double? Folate_mcg { get; set; }

    [Column("Vitamin_B12_mcg")]
    public double? VitaminB12_mcg { get; set; }

    [Column("Vitamin_B6_mg")]
    public double? VitaminB6_mg { get; set; }

    [Column("Vitamin_C_mg")]
    public double? VitaminC_mg { get; set; }

    [Column("Vitamin_D_mcg")]
    public double? VitaminD_mcg { get; set; }

    [Column("Vitamin_E_mg")]
    public double? VitaminE_mg { get; set; }

    [Column("Vitamin_K1_mcg")]
    public double? VitaminK1_mcg { get; set; }
    public double? Carotene_mcg{ get; set; }
    public double? Trytophan_mg { get; set; }

    public FoodItems? FoodItems { get; set; }
}

