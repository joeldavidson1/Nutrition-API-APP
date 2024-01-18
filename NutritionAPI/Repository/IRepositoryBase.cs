using System.Linq.Expressions;

namespace NutritionAPI.Interfaces;

public interface IRepositoryBase<T>
{
    IQueryable<T> FindAll(bool trackChanges);
    IQueryable<T> FindByCondition(Expression<Func<T, bool>> expression,
        bool trackChanges);
}