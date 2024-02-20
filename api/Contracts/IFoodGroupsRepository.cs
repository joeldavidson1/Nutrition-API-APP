using Entities.Models;
using Shared.RequestFeatures;

namespace Contracts;

public interface IFoodGroupsRepository
{
    Task<IEnumerable<FoodGroups>> GetAllFoodGroupsAsync(FoodGroupParameters foodGroupParameters, bool trackChanges);
    Task<FoodGroups> GetFoodGroupAsync(string foodGroupCode, bool trackChanges);
}