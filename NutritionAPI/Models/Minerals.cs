using System.ComponentModel.DataAnnotations;

namespace NutritionAPI.Models;

public class Minerals
{
    [Key]
    public string? FoodCode { get; set; }
    public string? Sodium { get; set; }
    public string? Potassium { get; set; }
    public string? Calcium { get; set; }
    public string? Magnesium { get; set; }
    public string? Phosporus { get; set; }
    public string? Iron { get; set; }
    public string? Copper { get; set; }
    public string? Zinc { get; set; }
    public string? Chloride { get; set; }
    public string? Manganese { get; set; }
    public string? Selenium { get; set; }
    public string? Iodine { get; set; }
    public FoodItems? FoodItem { get; set; }
}