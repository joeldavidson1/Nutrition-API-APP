namespace Entities.Exceptions;

public sealed class FoodItemNotFoundException : NotFoundException
{
    public FoodItemNotFoundException(string foodCode) : base($"The food item with Food Code: {foodCode} doesn't exist.")
    {
        
    }
}