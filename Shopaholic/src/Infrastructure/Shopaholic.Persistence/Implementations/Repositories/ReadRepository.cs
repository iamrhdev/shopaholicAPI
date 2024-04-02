using Microsoft.EntityFrameworkCore;
using Shopaholic.Application.Abstraction.Repositories;
using Shopaholic.Domain.Entities.Common;
using Shopaholic.Persistence.Context;
using System.Linq.Expressions;

namespace Shopaholic.Persistence.Implementations.Repositories
{
    public class ReadRepository<T> : IReadRepository<T> where T : BaseEntity, new()
    {
        private readonly ShopaholicDbContext _dbContext;

        public ReadRepository(ShopaholicDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public DbSet<T> Table => _dbContext.Set<T>();

        public IQueryable<T> GetAll(bool isTracking = true, params string[] includes)
        {
            var query = Table.AsQueryable();
            foreach (var include in includes)
            {
                query = query.Include(include);
            }
            return isTracking ? query : query.AsNoTracking();
        }

        public IQueryable<T> GetAllByExpression(Expression<Func<T, bool>> expression,
                                                int take,
                                                int skip,
                                                bool isTracking = true,
                                                params string[] includes)
        {
            var query = Table.Where(expression).Skip(skip).Take(take).AsQueryable();
            foreach (var include in includes)
            {
                query = query.Include(include);
            }
            return isTracking ? query : query.AsNoTracking();
        }

        public Task<List<T>> GetAllByExpressionAsync(Expression<Func<T, bool>> expression,
                                                     bool isOrdered = true,
                                                     bool isTracking = true,
                                                     params string[] includes)
        {
            var query = Table.Where(expression).AsQueryable();

            //query = isOrdered ? query.OrderBy(orderExpression) : query.OrderByDescending(orderExpression);
            foreach (var include in includes)
            {
                query = query.Include(include);
            }

            return isTracking ? query.AsTracking().ToListAsync() : query.AsNoTracking().ToListAsync();  // Need to check if AsNoTracking makes sense here since I use ToListAsync() - IEnumerable instead of IQueryable
        }

        public IQueryable<T> GetAllByExpressionOrderBy(Expression<Func<T, bool>> expression,
                                                       int take,
                                                       int skip,
                                                       Expression<Func<T, object>> expressionOrder,
                                                       bool isOrdered = true,
                                                       bool isTracking = true,
                                                       params string[] includes)
        {
            var query = Table.Where(expression).AsQueryable();
            query = isOrdered ? query.OrderBy(expressionOrder) : query.OrderByDescending(expression);
            query = query.Skip(skip).Take(take);
            foreach (var include in includes)
            {
                query = query.Include(include);
            }
            return isTracking ? query : query.AsNoTracking();
        }
        public async Task<T?> GetByExpressionAsync(Expression<Func<T, bool>> expression, bool isTracking = true)
        {
            var query = isTracking ? Table : Table.AsNoTracking();
            return await query.FirstOrDefaultAsync(expression);
        }
        public async Task<T?> GetByExpressionAsync(Expression<Func<T, bool>> expression, bool isTracking = true, params string[] includes)
        {
            var query = isTracking ? Table : Table.AsNoTracking();
            if (includes != null)
            {
                foreach (var include in includes)
                {
                    query = query.Include(include);
                }
            }
            return await query.SingleOrDefaultAsync(expression);
        }
        public async Task<T?> GetByIdAsync(Guid id) => await Table.FindAsync(id);

    }
}
