using Shared.DataTransferObjects;

namespace Service.Contracts;

public interface IFoodGroupsService
{
    IEnumerable<FoodGroupsDto> GetAllFoodGroups(bool trackChanges);
    FoodGroupsDto GetFoodGroup(string foodCodeGroup, bool trackChanges);
}