using Entities.Models;

namespace Contracts;

public interface IFoodGroupsRepository
{
    Task<IEnumerable<FoodGroups>> GetAllFoodGroupsAsync(bool trackChanges);
    Task<FoodGroups> GetFoodGroupAsync(string foodGroupCode, bool trackChanges);
}