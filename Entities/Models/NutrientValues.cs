using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Security.AccessControl;

namespace Entities.Models;

public class NutrientValues
{
    [Key]
    public string? ValuesId { get; set; }
    public string? FoodCode { get; set; }
    public string? NutrientId { get; set; }
    public string? Value { get; set; }
    [ForeignKey("FoodCode")]
    public FoodItems? FoodItems { get; set; }
    [ForeignKey("NutrientId")]
    public NutrientsAndEnergy? NutrientsAndEnergy { get; set; }
}