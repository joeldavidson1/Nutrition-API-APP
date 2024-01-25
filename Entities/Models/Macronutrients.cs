using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities.Models;

public class Macronutrients
{
    [Key]
    public string? FoodCode { get; set; }
    public double? Protein_g { get; set; }
    public double? Fat_g { get; set; }
    public double? Carbohydrate_g { get; set; }
    public FoodItems? FoodItems { get; set; }
}