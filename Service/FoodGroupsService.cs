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

    public async Task<IEnumerable<FoodGroupsDto>> GetAllFoodGroups(bool trackChanges)
    {
        IEnumerable<FoodGroups> foodGroups = await _repository.FoodGroups.GetAllFoodGroupsAsync(trackChanges);
        IEnumerable<FoodGroupsDto> foodGroupsDtos = _mapper.MapFoodGroupsToDto(foodGroups);

        return foodGroupsDtos;
    }

    public async Task<FoodGroupsDto> GetFoodGroup(string foodGroupCode, bool trackChanges)
    {
        FoodGroups foodGroup = await _repository.FoodGroups.GetFoodGroupAsync(foodGroupCode, trackChanges);
        if (foodGroup is null)
            throw new FoodGroupNotFoundException(foodGroupCode);

        FoodGroupsDto foodGroupDto = _mapper.MapFoodGroupToDto(foodGroup);
        return foodGroupDto;
    }
}