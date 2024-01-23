using AutoMapper;
using Contracts;
using Entities.Exceptions;
using Entities.Models;
using Service.Contracts;
using Shared.DataTransferObjects;
using Shared.RequestFeatures;

namespace Service;

internal sealed class FoodItemsService : IFoodItemsService
{
    private readonly IRepositoryManager _repository;
    private readonly IMapper _mapper;

    public FoodItemsService(IRepositoryManager repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<(IEnumerable<FoodItemsDto> foodItems, MetaData metaData)> GetAllFoodItemsAsync(FoodItemParameters foodItemParameters, bool trackChanges)
    {
        PagedList<FoodItems> foodItemsWithMetaData = await _repository.FoodItems.GetAllFoodItemsAsync(foodItemParameters,
            trackChanges);
        IEnumerable<FoodItemsDto>? foodItemsDto = _mapper.Map<IEnumerable<FoodItemsDto>>(foodItemsWithMetaData);

        return (foodItems: foodItemsDto, metaData: foodItemsWithMetaData.MetaData);
    }

    public async Task<FoodItemsDto>GetFoodItemAsync(string foodCode, bool trackChanges)
    {
        FoodItems? foodItem = await _repository.FoodItems.GetFoodItemAsync(foodCode, trackChanges);
        if (foodItem is null) 
            throw new FoodItemNotFoundException(foodCode);

        FoodItemsDto foodItemDto = _mapper.Map<FoodItemsDto>(foodItem);
        return foodItemDto;
    }

    public async Task<IEnumerable<FoodItemsDto>> GetFoodItemsForFoodGroupAsync(string foodGroupCode, bool trackChanges)
    {
        IEnumerable<FoodItems> foodItems = await _repository.FoodItems.GetFoodItemsForFoodGroupAsync(foodGroupCode, trackChanges);
        IEnumerable<FoodItemsDto> foodItemsDto = _mapper.Map<IEnumerable<FoodItemsDto>>(foodItems);
        
        return foodItemsDto;
    }
}