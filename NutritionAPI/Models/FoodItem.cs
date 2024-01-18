using System.ComponentModel.DataAnnotations;

namespace NutritionAPI.Models;

public class FoodItem
{
    [Key]
    public string? FoodCode { get; set; }
    public string? Name { get; set; }
    public string? Description { get; set; }
    public string? DataReferences { get; set; }
    public string? FoodGroupCode { get; set; }
    public FoodGroup? FoodGroup { get; set; }
    public MacronutrientsAndEnergy? MacronutrientsAndEnergy { get; set; }
    public Proximates? Proximates { get; set; }
    public Minerals? Minerals { get; set; }
    public Vitamins? Vitamins { get; set; }
}