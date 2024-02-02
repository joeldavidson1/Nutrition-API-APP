using AutoMapper;
using Contracts;
using Entities.Exceptions;
using Entities.Models;
using Service.Contracts;
using Shared.DataTransferObjects;
using Shared.RequestFeatures;

namespace Service;

internal sealed class FoodGroupsService : IFoodGroupsService
{
    private readonly IRepositoryManager _repository;
    private readonly IMapper _mapper;

    public FoodGroupsService(IRepositoryManager repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<IEnumerable<FoodGroupsDto>> GetAllFoodGroups(FoodGroupParameters foodGroupParameters, 
        bool trackChanges)
    {
        IEnumerable<FoodGroups> foodGroups = await _repository.FoodGroups.GetAllFoodGroupsAsync(foodGroupParameters, 
            trackChanges);
        var foodGroupsDtos = _mapper.Map<IEnumerable<FoodGroupsDto>>(foodGroups);

        return foodGroupsDtos;
    }

    public async Task<FoodGroupsDto> GetFoodGroup(string foodGroupCode, bool trackChanges)
    {
        FoodGroups foodGroup = await _repository.FoodGroups.GetFoodGroupAsync(foodGroupCode, trackChanges);
        if (foodGroup is null)
            throw new FoodGroupNotFoundException(foodGroupCode);

        FoodGroupsDto foodGroupDto = _mapper.Map<FoodGroupsDto>(foodGroup);
        return foodGroupDto;
    }
}