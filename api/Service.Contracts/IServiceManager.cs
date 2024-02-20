namespace Service.Contracts;

public interface IServiceManager
{
    IFoodItemsService FoodItemsService { get; }
    IFoodGroupsService FoodGroupsService { get; }
}