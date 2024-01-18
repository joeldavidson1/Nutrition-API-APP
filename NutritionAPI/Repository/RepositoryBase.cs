using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using NutritionAPI.Data;
using NutritionAPI.Interfaces;

namespace NutritionAPI.Repository;

public abstract class RepositoryBase<T> : IRepositoryBase<T> where T : class
{
    protected DataContext DataContext;

    public RepositoryBase(DataContext dataContext) => DataContext = dataContext;

    public IQueryable<T> FindAll(bool trackChanges) =>
        !trackChanges ? DataContext.Set<T>().AsNoTracking() : DataContext.Set<T>();

    public IQueryable<T> FindByCondition(Expression<Func<T, bool>> expression, bool trackChanges) =>
        !trackChanges ? DataContext.Set<T>().Where(expression).AsNoTracking() : DataContext.Set<T>().Where(expression);
}