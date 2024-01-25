using System.Linq.Dynamic.Core;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;

namespace Repository.Extensions;

public static class RepositoryFoodItemsExtensions
{
    public static IQueryable<FoodItems> Search(this IQueryable<FoodItems> foodItems, string orderByQuery)
    {
        if (string.IsNullOrWhiteSpace(orderByQuery))
            return foodItems;
    
        string lowerCaseTerm = orderByQuery.Trim().ToLower();
    
        return foodItems.Where(fi => fi.Name.ToLower().Contains(lowerCaseTerm));
    }

    public static IQueryable<FoodItems> Sort(this IQueryable<FoodItems> foodItems, string orderByQueryString)
    {
        if (string.IsNullOrWhiteSpace(orderByQueryString))
            return foodItems.OrderBy(e => e.Name);

        string[] orderParams = orderByQueryString.ToLower().Split(' ');

        if (orderParams[0] != "name")
        {
            try
            {
                string modelName = orderParams[0];
                string propertyName = orderParams[1];
                bool isDescending = orderParams.Length > 2 &&
                                    (orderParams[2] == "desc" || orderParams[2] == "descending");

                var parameter = Expression.Parameter(typeof(FoodItems), "fi");
                var property = Expression.PropertyOrField(parameter, modelName);

                // Expression to represent null check
                var nullCheck = Expression.Equal(Expression.PropertyOrField(property, propertyName),
                    Expression.Constant(null));

                // Expression to represent property access with appropriate casting
                var propertyAccess =
                    Expression.Convert(Expression.PropertyOrField(property, propertyName), typeof(double?));

                // Combine null check and property access using Expression.Condition
                var condition = Expression.Condition(nullCheck,
                    Expression.Constant(0.0, typeof(double?)),
                    propertyAccess
                );

                // Create the final lambda expression
                var orderByExpression = Expression.Lambda<Func<FoodItems, double?>>(condition, parameter);
                Console.WriteLine(orderByExpression);

                var orderedQuery = isDescending
                    ? foodItems.OrderByDescending(orderByExpression)
                    : foodItems.OrderBy(orderByExpression);

                return orderedQuery;
            }
            catch (Exception)
            {
                return foodItems.OrderBy(fi => fi.Name);
            }
        }

        if (orderParams.Last() == "desc" || orderParams.Last() == "descending")
            return foodItems.OrderByDescending(fi => fi.Name);

        return foodItems.OrderBy(fi => fi.Name);
    }
}