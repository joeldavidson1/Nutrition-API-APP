using System.ComponentModel.DataAnnotations;

namespace NutritionAPI.Models;

public class MacronutrientsAndEnergy
{
    [Key]
    public string? FoodCode { get; set; }
    public string? EnergyKcal { get; set; }
    public string? EnergyKj { get; set; }
    public string? Protein { get; set; } 
    public string? Fat { get; set; }
    public string? Carbohydrate { get; set; }
    public FoodItems? FoodItem { get; set; }
}