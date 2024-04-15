namespace Shared.RequestFeatures;

public class FoodItemParameters : RequestParameters
{
    /// <summary>
    /// Search for a food item by name
    /// </summary>
    /// <example>Banana</example>
    public string? SearchFoodByName { get; set; }
    
    /// <summary>
    /// Order the food items by a specific nutrient using its parent container in descending or ascending order 
    /// </summary>
    /// <example>'macronutrients protein_g desc' or 'proximates fructose_g desc'</example> 
    public string? OrderBy { get; set; }
}