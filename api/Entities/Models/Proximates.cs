using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities.Models;

public class Proximates
{
    [Key]
    public string? FoodCode { get; set; }
    public double? Water_g { get; set; }
    public double? Starch_g { get; set; }
    [Column("Total_Sugars_g")]
    public double? TotalSugars_g { get; set; }
    public double? Glucose_g { get; set; }
    public double? Galactose_g { get; set; }
    public double? Fructose_g { get; set; }
    public double? Sucrose_g { get; set; }
    public double? Maltose_g { get; set; }
    public double? Lactose_g { get; set; }
    public double? Alcohol_g { get; set; }

    [Column("Non_Starch_Polysaccharides_g")]
    public double? NonStarchPolysaccharides_g { get; set; }
    public double? Fibre_g { get; set; }
    
    [Column("Fats_Saturated_g")]
    public double? FatsSaturated_g { get; set; }

    [Column("Fats_Monounsaturated_g")]
    public double? FatsMonounsaturated_g { get; set; }

    [Column("Fats_Polyunsaturated_g")]
    public double? FatsPolyunsaturated_g { get; set; }

    [Column("Fats_Trans_g")]
    public double? FatsTrans_g { get; set; }
    public double? Cholesterol_g { get; set; }
    public FoodItems? FoodItems { get; set; }
}