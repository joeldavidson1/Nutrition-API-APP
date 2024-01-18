using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace NutritionAPI.Models;

public class FoodGroup
{
    [Key]
    public string? FoodGroupCode { get; set; }
    public string? Description { get; set; }
    public ICollection<FoodItems>? FoodItems { get; set; }
}