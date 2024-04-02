using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Shopaholic.Domain.Entities.Common;
using Shopaholic.Domain.Identity;

namespace Shopaholic.Persistence.Context
{
    public class ShopaholicDbContext : IdentityDbContext<AppUser>
    {
        public ShopaholicDbContext(DbContextOptions<ShopaholicDbContext> options) : base(options) { }
        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            var datas = ChangeTracker.Entries<BaseEntity>();
            foreach (var data in datas)
            {
                switch (data.State)
                {
                    case EntityState.Added:
                        data.Entity.DateCreated = DateTime.UtcNow;
                        break;
                    case EntityState.Modified:
                        data.Entity.DateModified = DateTime.UtcNow;
                        break;
                    default:
                        break;
                }
            }
            return await base.SaveChangesAsync(cancellationToken);
        }
    }
}
