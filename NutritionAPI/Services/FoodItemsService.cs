using NutritionAPI.Dto;
using NutritionAPI.Interfaces;
using NutritionAPI.Models;

namespace NutritionAPI.Services;

public sealed class FoodItemsService : IFoodItemsService
{
    private readonly IRepositoryManager _repository;
    private readonly IMappingService _mapper;

    public FoodItemsService(IRepositoryManager repository, IMappingService mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }


    public async Task<IEnumerable<FoodItemsDto>> GetAllFoodItemsAsync(FoodItemsParameters foodItemsParameters, bool trackChanges)
    {
        IEnumerable<FoodItems> foodItems = await _repository.FoodItems.GetAllFoodItemsAsync(foodItemsParameters, trackChanges);
        IEnumerable<FoodItemsDto> foodItemsDto = _mapper.MapFoodItemsToDtos(foodItems);

        return foodItemsDto;
    }
}