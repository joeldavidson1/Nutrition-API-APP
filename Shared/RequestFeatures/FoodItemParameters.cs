namespace Shared.RequestFeatures;

public class FoodItemParameters : RequestParameters
{
    public FoodItemParameters() => OrderBy = "name";
    
    // public double MinNutrientValue { get; set; }
    // public double MaxNutrientValue { get; set; } = int.MaxValue;
    //
    // public bool ValidNutrientValueRange => MaxNutrientValue > MinNutrientValue;

    public string? SearchFoodByName { get; set; }
}