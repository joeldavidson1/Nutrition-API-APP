using NutritionAPI.Data;
using NutritionAPI.Interfaces;

namespace NutritionAPI.Repository;

public class RepositoryManager : IRepositoryManager
{
    private readonly DataContext _dataContext;
    private readonly Lazy<IFoodItemRepository> _foodItemRepository;

    public RepositoryManager(DataContext dataContext)
    {
        _dataContext = dataContext;
        _foodItemRepository = new Lazy<IFoodItemRepository>(() => new FoodItemsRepository(dataContext));
    }

    public IFoodItemRepository FoodItems => _foodItemRepository.Value;
}