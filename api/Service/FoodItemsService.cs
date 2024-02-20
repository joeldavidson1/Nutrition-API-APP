using System.Dynamic;
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
    private readonly IDataShaper<FoodItemsDto> _dataShaper;

    public FoodItemsService(IRepositoryManager repository, IMapper mapper, IDataShaper<FoodItemsDto> dataShaper)
    {
        _repository = repository;
        _mapper = mapper;
        _dataShaper = dataShaper;
    }

    public async Task<(IEnumerable<ExpandoObject> foodItems, MetaData metaData)> 
        GetAllFoodItemsAsync(FoodItemParameters foodItemParameters, bool trackChanges)
    {
        PagedList<FoodItems> foodItemsWithMetaData = await _repository.FoodItems.GetAllFoodItemsAsync(foodItemParameters,
            trackChanges);
        IEnumerable<FoodItemsDto>? foodItemsDto = _mapper.Map<IEnumerable<FoodItemsDto>>(foodItemsWithMetaData);

        IEnumerable<ExpandoObject> shapedData = _dataShaper.ShapeData(foodItemsDto, foodItemParameters.Fields);

        return (foodItems: shapedData, metaData: foodItemsWithMetaData.MetaData);
    }

    public async Task<FoodItemsDto>GetFoodItemAsync(string foodCode, bool trackChanges)
    {
        FoodItems? foodItem = await _repository.FoodItems.GetFoodItemAsync(foodCode, trackChanges);
        if (foodItem is null) 
            throw new FoodItemNotFoundException(foodCode);

        FoodItemsDto foodItemDto = _mapper.Map<FoodItemsDto>(foodItem);
        return foodItemDto;
    }

    public async Task<(IEnumerable<ExpandoObject> foodItems, MetaData metaData)>  
        GetFoodItemsForFoodGroupAsync(FoodItemParameters foodItemParameters,
        string foodGroupCode, bool trackChanges)
    {
        // IEnumerable<FoodItems> foodItems = await _repository.FoodItems.GetFoodItemsForFoodGroupAsync(foodItemParameters,
        //     foodGroupCode, trackChanges);
        // IEnumerable<FoodItemsDto> foodItemsDto = _mapper.Map<IEnumerable<FoodItemsDto>>(foodItems);
        //
        // return foodItemsDto;
        PagedList<FoodItems> foodItemsWithMetaData =
            await _repository.FoodItems.GetFoodItemsForFoodGroupAsync(foodItemParameters, foodGroupCode, trackChanges);
        
        IEnumerable<FoodItemsDto>? foodItemsDto = _mapper.Map<IEnumerable<FoodItemsDto>>(foodItemsWithMetaData);
        
        IEnumerable<ExpandoObject> shapedData = _dataShaper.ShapeData(foodItemsDto, foodItemParameters.Fields);
        
        return (foodItems: shapedData, metaData: foodItemsWithMetaData.MetaData);
    }
}