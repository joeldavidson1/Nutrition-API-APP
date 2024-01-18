using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Authentication;

namespace NutritionAPI.Models;

public class Proximates
{
    [Key]
    public string? FoodCode { get; set; }
    public string? Water { get; set; }
    public string? Starch { get; set; }
    public string? TotalSugars { get; set; }
    public string? Glucose { get; set; }
    public string? Galactose { get; set; }
    public string? Fructose { get; set; }
    public string? Sucrose { get; set; }
    public string? Maltose { get; set; }
    public string? Lactose { get; set; }
    public string? Alochol { get; set; }
    public string? NonStarchPolysaccharides { get; set; }
    public string? FibreAOAC { get; set; }
    public string? FatsSaturated { get; set; }
    public string? FatsPolyunsaturated { get; set; }
    public string? FatsTrans { get; set; }
    public string? Cholesterol { get; set; }
    public FoodItem? FoodItem { get; set; }
}