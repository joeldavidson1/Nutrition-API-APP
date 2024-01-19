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

    public IEnumerable<FoodItemsDto> GetAllFoodItems(bool trackChanges)
    {
        IEnumerable<FoodItems> foodItems = _repository.FoodItems.GetAllFoodItems(trackChanges);
        IEnumerable<FoodItemsDto> foodItemsDto = _mapper.MapFoodItemsToDto(foodItems);

        return foodItemsDto;
    }

    public FoodItemsDto GetFoodItem(string foodCode, bool trackChanges)
    {
        FoodItems foodItem = _repository.FoodItems.GetFoodItem(foodCode, trackChanges);
        if (foodItem is null) 
            throw new FoodItemNotFoundException(foodCode);
        
        FoodItemsDto foodItemDto = _mapper.MapFoodItemToDto(foodItem);
        return foodItemDto;
    }

    public IEnumerable<FoodItemsDto> GetFoodItemsForFoodGroup(string foodGroupCode, bool trackChanges)
    {
        IEnumerable<FoodItems> foodItems = _repository.FoodItems.GetFoodItemsForFoodGroup(foodGroupCode, trackChanges);
        IEnumerable<FoodItemsDto> foodItemsDto = _mapper.MapFoodItemsToDto(foodItems);
        
        return foodItemsDto;
    }
}