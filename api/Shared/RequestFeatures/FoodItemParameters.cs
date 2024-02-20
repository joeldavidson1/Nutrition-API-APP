namespace Shared.RequestFeatures;

public class FoodItemParameters : RequestParameters
{
    // public double MinNutrientValue { get; set; }
    // public double MaxNutrientValue { get; set; } = int.MaxValue;
    //
    // public bool ValidNutrientValueRange => MaxNutrientValue > MinNutrientValue;

    public string? SearchFoodByName { get; set; }
    public string? Fields { get; set; }
    public string? OrderBy { get; set; }
}