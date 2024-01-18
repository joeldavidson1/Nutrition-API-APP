namespace NutritionAPI.Interfaces;

public interface IRepositoryManager
{
    IFoodItemRepository FoodItems { get; }
}