namespace NutritionAPI.Services;

public interface IServiceManager
{
    IFoodItemsService FoodItemsService { get; }
}