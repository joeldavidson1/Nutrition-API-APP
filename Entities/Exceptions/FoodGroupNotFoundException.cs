namespace Entities.Exceptions;

public sealed class FoodGroupNotFoundException : NotFoundException
{
    public FoodGroupNotFoundException(string foodGroupCode) : 
        base($"The food group with Food Group Code: {foodGroupCode} doesn't exist.")
    {
    }
}