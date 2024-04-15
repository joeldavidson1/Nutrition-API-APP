namespace Shared.RequestFeatures;

public class FoodGroupParameters : RequestParameters
{
    /// <summary>
    /// Search for a food group by its name
    /// </summary>
    /// <example>Vegetables</example>
    public string? SearchFoodGroupByDescription { get; set; }
}