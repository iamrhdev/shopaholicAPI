using Microsoft.EntityFrameworkCore;
using Shopaholic.Application.Abstraction.Repositories;
using Shopaholic.Domain.Entities.Common;
using Shopaholic.Persistence.Context;

namespace Shopaholic.Persistence.Implementations.Repositories
{
    public class WriteRepository<T> : IWriteRepository<T> where T : BaseEntity, new()
    {
        private readonly ShopaholicDbContext _dbContext;
        public DbSet<T> Table => _dbContext.Set<T>();


        public WriteRepository(ShopaholicDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task AddAsync(T entity) => await Table.AddAsync(entity);

        public async Task AddRangeAsync(ICollection<T> entities) => await Table.AddRangeAsync(entities);

        public void Remove(T entity) => Table.Remove(entity);

        public void RemoveRange(ICollection<T> entities) => Table.RemoveRange(entities);

        public async Task SaveChangeAsync() => await _dbContext.SaveChangesAsync();

        public void Update(T entity) => Table.Update(entity);
    }
}
