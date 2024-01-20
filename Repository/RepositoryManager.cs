using Contracts;

namespace Repository;

public sealed class RepositoryManager : IRepositoryManager
{
    private readonly RepositoryContext _repositoryContext;
    private readonly Lazy<IFoodItemsRepository> _foodItemsRepository;
    private readonly Lazy<IFoodGroupsRepository> _foodGroupsRepository;

    public RepositoryManager(RepositoryContext repositoryContext)
    {
        _repositoryContext = repositoryContext;
        _foodItemsRepository = new Lazy<IFoodItemsRepository>(() => new FoodItemsRepository(repositoryContext));
        _foodGroupsRepository = new Lazy<IFoodGroupsRepository>(() => new FoodGroupsRepository(repositoryContext));
    }

    public IFoodItemsRepository FoodItems => _foodItemsRepository.Value;
    public IFoodGroupsRepository FoodGroups => _foodGroupsRepository.Value;

    public Task SaveAsync() => _repositoryContext.SaveChangesAsync();
}