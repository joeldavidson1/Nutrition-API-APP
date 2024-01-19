using Contracts;
using Entities.Exceptions;
using Entities.Models;
using Service.Contracts;
using Shared.DataTransferObjects;

namespace Service;

internal sealed class FoodGroupsService : IFoodGroupsService
{
    private readonly IRepositoryManager _repository;
    private readonly IMapperService _mapper;

    public FoodGroupsService(IRepositoryManager repository, IMapperService mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public IEnumerable<FoodGroupsDto> GetAllFoodGroups(bool trackChanges)
    {
        IEnumerable<FoodGroups> foodGroups = _repository.FoodGroups.GetAllFoodGroups(trackChanges);
        IEnumerable<FoodGroupsDto> foodGroupsDtos = _mapper.MapFoodGroupsToDto(foodGroups);

        return foodGroupsDtos;
    }

    public FoodGroupsDto GetFoodGroup(string foodGroupCode, bool trackChanges)
    {
        FoodGroups foodGroup = _repository.FoodGroups.GetFoodGroup(foodGroupCode, trackChanges);
        if (foodGroup is null)
            throw new FoodGroupNotFoundException(foodGroupCode);

        FoodGroupsDto foodGroupDto = _mapper.MapFoodGroupToDto(foodGroup);
        return foodGroupDto;
    }
}

// private readonly IRepositoryManager _repository;
// private readonly IMapperService _mapper;
//
// public FoodItemsService(IRepositoryManager repository, IMapperService mapper)
// {
//     _repository = repository;
//     _mapper = mapper;
// }
//
// public IEnumerable<FoodItemsDto> GetAllFoodItems(bool trackChanges)
// {
//     IEnumerable<FoodItems> foodItems = _repository.FoodItems.GetAllFoodItems(trackChanges);
//     IEnumerable<FoodItemsDto> foodItemsDto = _mapper.MapFoodItemsToDtos(foodItems);
//
//     return foodItemsDto;
// }
//
// public FoodItemsDto GetFoodItem(string foodCode, bool trackChanges)
// {
//     FoodItems foodItem = _repository.FoodItems.GetFoodItem(foodCode, trackChanges);
//     if (foodItem is null) 
//         throw new FoodItemNotFoundException(foodCode);
//         
//     FoodItemsDto foodItemDto = _mapper.MapFoodItemToDto(foodItem);
//     return foodItemDto;
// }