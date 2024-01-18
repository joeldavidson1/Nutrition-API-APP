using System.ComponentModel.DataAnnotations;

namespace NutritionAPI.Models;

public class Vitamins
{
    [Key]
    public string? FoodCode { get; set; }
    public string? Retinol { get; set; }
    public string? Thiamin{ get; set; }
    public string? Riboflavin { get; set; }
    public string? Niacin { get; set; }
    public string? Pantothenate { get; set; }
    public string? VitaminB6 { get; set; }
    public string? Biotin { get; set; }
    public string? Folate { get; set; }
    public string? VitaminB12 { get; set; }
    public string? VitaminC { get; set; }
    public string? VitaminD { get; set; }
    public string? VitaminE { get; set; }
    public string? VitaminK1 { get; set; }
    public string? Carotene { get; set; }
    public string? Trytophan { get; set; }
    public FoodItems? FoodItem { get; set; }
}