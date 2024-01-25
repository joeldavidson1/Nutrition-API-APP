using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities.Models;

public class Minerals
{
    [Key]
    public string? FoodCode { get; set; }
    public double? Sodium_mg { get; set; }
    public double? Potassium_mg { get; set; }
    public double? Calcium_mg { get; set; }
    public double? Magnesium_mg { get; set; }
    public double? Phosphorus_mg { get; set; }
    public double? Iron_mg { get; set; }
    public double? Copper_mg { get; set; }
    public double? Zinc_mg { get; set; }
    public double? Chloride_mg { get; set; }
    public double? Manganese_mg { get; set; }
    public double? Selenium_mcg { get; set; }
    public double? Iodine_mcg { get; set; }

    public FoodItems? FoodItems { get; set; }
}