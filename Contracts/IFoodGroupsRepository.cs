using Entities.Models;

namespace Contracts;

public interface IFoodGroupsRepository
{
    IEnumerable<FoodGroups> GetAllFoodGroups(bool trackChanges);
    FoodGroups GetFoodGroup(string foodGroupCode, bool trackChanges);
}