using Contracts;
using Service.Contracts;

namespace Service;

public sealed class ServiceManager : IServiceManager
{
    private readonly Lazy<IFoodItemsService> _foodItemsService;
    private readonly Lazy<IFoodGroupsService> _foodGroupsService;

    public ServiceManager(IRepositoryManager repositoryManager, IMapperService mapper)
    {
        _foodItemsService = new Lazy<IFoodItemsService>(() => new FoodItemsService(repositoryManager, mapper));
        _foodGroupsService = new Lazy<IFoodGroupsService>(() => new FoodGroupsService(repositoryManager, mapper));
    }

    public IFoodItemsService FoodItemsService => _foodItemsService.Value;
    public IFoodGroupsService FoodGroupsService => _foodGroupsService.Value;
}