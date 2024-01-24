namespace Entities.Exceptions;

public sealed class MaxNutrientRangeBadRequestException : BadRequestException
{
    public MaxNutrientRangeBadRequestException() : base("Max value can't be less than min value.") {}
}