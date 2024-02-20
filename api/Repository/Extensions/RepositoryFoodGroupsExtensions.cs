using Entities.Models;

namespace Repository.Extensions;

public static class RepositoryFoodGroupsExtensions
{
    public static IQueryable<FoodGroups> Search(this IQueryable<FoodGroups> foodGroups, string orderByQuery)
    {
        if (string.IsNullOrWhiteSpace(orderByQuery))
            return foodGroups;
    
        string lowerCaseTerm = orderByQuery.Trim().ToLower();
    
        return foodGroups.Where(fi => fi.Description.ToLower().Contains(lowerCaseTerm));
    }
}