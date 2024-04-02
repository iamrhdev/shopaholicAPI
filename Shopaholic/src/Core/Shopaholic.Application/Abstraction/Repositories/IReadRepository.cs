using Shopaholic.Domain.Entities.Common;
using System.Linq.Expressions;

namespace Shopaholic.Application.Abstraction.Repositories
{
    public interface IReadRepository<T> : IRepository<T> where T : BaseEntity, new()
    {
        IQueryable<T> GetAll(bool isTracking = true, params string[] includes);
        IQueryable<T> GetAllByExpression(Expression<Func<T, bool>> expression, int take, int skip, bool isTracking = true, params string[] includes);
        IQueryable<T> GetAllByExpressionOrderBy(Expression<Func<T, bool>> expression, int take, int skip, Expression<Func<T, object>> expressionOrder, bool isOrdered = true, bool isTracking = true, params string[] includes);
        Task<List<T>> GetAllByExpressionAsync(Expression<Func<T, bool>> expression, bool isOrdered = true, bool isTracking = true, params string[] includes);
        Task<T?> GetByExpressionAsync(Expression<Func<T, bool>> expression, bool isTracking = true, params string[] include);
        Task<T?> GetByIdAsync(Guid id);
        Task<T?> GetByExpressionAsync(Expression<Func<T, bool>> expression, bool isTracking = true);
    }
}
