using Contracts;
using Entities.Exceptions;
using Entities.Models;
using Service.Contracts;
using Shared.DataTransferObjects;

namespace Service;

internal sealed class FoodItemsService : IFoodItemsService
{
    private readonly IRepositoryManager _repository;
    private readonly IMapperService _mapper;

    public FoodItemsService(IRepositoryManager repository, IMapperService mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<IEnumerable<FoodItemsDto>> GetAllFoodItemsAsync(bool trackChanges)
    {
        IEnumerable<FoodItems> foodItems = await _repository.FoodItems.GetAllFoodItemsAsync(trackChanges);
        IEnumerable<FoodItemsDto> foodItemsDto = _mapper.MapFoodItemsToDto(foodItems);

        return foodItemsDto;
    }

    public async Task<FoodItemsDto>GetFoodItemAsync(string foodCode, bool trackChanges)
    {
        FoodItems? foodItem = await _repository.FoodItems.GetFoodItemAsync(foodCode, trackChanges);
        if (foodItem is null) 
            throw new FoodItemNotFoundException(foodCode);
        
        FoodItemsDto foodItemDto = _mapper.MapFoodItemToDto(foodItem);
        return foodItemDto;
    }

    public async Task<IEnumerable<FoodItemsDto>> GetFoodItemsForFoodGroupAsync(string foodGroupCode, bool trackChanges)
    {
        IEnumerable<FoodItems> foodItems = await _repository.FoodItems.GetFoodItemsForFoodGroupAsync(foodGroupCode, trackChanges);
        IEnumerable<FoodItemsDto> foodItemsDto = _mapper.MapFoodItemsToDto(foodItems);
        
        return foodItemsDto;
    }
}