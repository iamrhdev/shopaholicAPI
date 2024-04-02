using Microsoft.EntityFrameworkCore;
using Shopaholic.Domain.Entities.Common;

namespace Shopaholic.Application.Abstraction.Repositories
{
    public interface IRepository<T> where T : BaseEntity, new()
    {
        public DbSet<T> Table { get; }
    }
}
