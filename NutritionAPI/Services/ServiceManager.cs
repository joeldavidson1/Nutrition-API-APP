using NutritionAPI.Interfaces;

namespace NutritionAPI.Services;

public sealed class ServiceManager : IServiceManager
{
    private readonly Lazy<IFoodItemsService> _foodItemsService;

    public ServiceManager(IRepositoryManager repositoryManager, IMappingService mapper)
    {
        _foodItemsService = new Lazy<IFoodItemsService>(() => new FoodItemsService(repositoryManager, mapper));
    }

    public IFoodItemsService FoodItemsService => _foodItemsService.Value;
}