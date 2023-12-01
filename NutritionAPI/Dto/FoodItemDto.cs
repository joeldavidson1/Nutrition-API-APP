using NutritionAPI.Models;

namespace NutritionAPI.Dto;

public class FoodItemDto
{
    public string FoodCode { get; set; }
    public string Name { get; set; }
    public string FoodGroupCode { get; set; }
    public FoodGroupDto FoodGroup { get; set; }
}