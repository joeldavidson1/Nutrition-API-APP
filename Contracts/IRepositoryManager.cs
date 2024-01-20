namespace Contracts;

public interface IRepositoryManager
{
    IFoodItemsRepository FoodItems { get; }
    IFoodGroupsRepository FoodGroups { get; }
    Task SaveAsync();
}