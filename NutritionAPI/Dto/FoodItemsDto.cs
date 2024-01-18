using NutritionAPI.Models;

namespace NutritionAPI.Dto;

public record FoodItemsDto
{
    public string FoodCode { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public string DataReferences { get; set; }
    public FoodGroupDto FoodGroup { get; set; }
    public ProximatesDto Proximates { get; set; }
}