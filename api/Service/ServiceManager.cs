using AutoMapper;
using Contracts;
using Service.Contracts;
using Shared.DataTransferObjects;

namespace Service;

public sealed class ServiceManager : IServiceManager
{
    private readonly Lazy<IFoodItemsService> _foodItemsService;
    private readonly Lazy<IFoodGroupsService> _foodGroupsService;

    public ServiceManager(IRepositoryManager repositoryManager, IMapper mapper, IDataShaper<FoodItemsDto> dataShaper)
    {
        _foodItemsService = new Lazy<IFoodItemsService>(() => new FoodItemsService(repositoryManager, mapper, dataShaper));
        _foodGroupsService = new Lazy<IFoodGroupsService>(() => new FoodGroupsService(repositoryManager, mapper));
    }

    public IFoodItemsService FoodItemsService => _foodItemsService.Value;
    public IFoodGroupsService FoodGroupsService => _foodGroupsService.Value;
}