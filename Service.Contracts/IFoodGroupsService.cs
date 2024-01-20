using Shared.DataTransferObjects;

namespace Service.Contracts;

public interface IFoodGroupsService
{
    Task<IEnumerable<FoodGroupsDto>> GetAllFoodGroups(bool trackChanges);
    Task<FoodGroupsDto> GetFoodGroup(string foodCodeGroup, bool trackChanges);
}