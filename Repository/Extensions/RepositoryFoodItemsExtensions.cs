using Entities.Models;

namespace Repository.Extensions;

public static class RepositoryFoodItemsExtensions
{
    public static IQueryable<FoodItems> Search(this IQueryable<FoodItems> foodItems, string searchTerm)
    {
        if (string.IsNullOrWhiteSpace(searchTerm))
            return foodItems;

        string lowerCaseTerm = searchTerm.Trim().ToLower();

        return foodItems.Where(fi => fi.Name.ToLower().Contains(lowerCaseTerm));
    }
}