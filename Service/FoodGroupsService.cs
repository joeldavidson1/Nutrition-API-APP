using Contracts;
using Service.Contracts;

namespace Service;

internal sealed class FoodGroupsService : IFoodGroupsService
{
    private readonly IRepositoryManager _repository;

    public FoodGroupsService(IRepositoryManager repository)
    {
        _repository = repository;
    }
}