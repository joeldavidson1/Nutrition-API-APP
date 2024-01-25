using System.ComponentModel.DataAnnotations;

namespace Entities.Models;

public class Energy
{
    [Key]
    public string FoodCode { get; set; }
    public double? Kcal { get; set; }
    public double? Kj { get; set; }
    public FoodItems? FoodItems { get; set; }
}