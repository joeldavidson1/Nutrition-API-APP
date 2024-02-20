using Shared.DataTransferObjects;
using Shared.RequestFeatures;

namespace Service.Contracts;

public interface IFoodGroupsService
{
    Task<IEnumerable<FoodGroupsDto>> GetAllFoodGroups(FoodGroupParameters foodGroupParameters, bool trackChanges);
    Task<FoodGroupsDto> GetFoodGroup(string foodCodeGroup, bool trackChanges);
}