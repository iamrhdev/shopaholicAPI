using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Shopaholic.Domain.Enums;
using Shopaholic.Domain.Identity;

namespace Shopaholic.Persistence.Context
{
    public class ShopaholicDbContextInitialiser
    {
        private readonly ShopaholicDbContext _context;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<AppUser> _userManager; 
        private readonly IConfiguration _configuration;

        public ShopaholicDbContextInitialiser(ShopaholicDbContext context,
                                              RoleManager<IdentityRole> roleManager,
                                              UserManager<AppUser> userManager,
                                              IConfiguration configuration)
        {
            _context = context;
            _roleManager = roleManager;
            _userManager = userManager;
            _configuration = configuration;
        }
        public async Task InitialiseAsync()
        {
            await _context.Database.MigrateAsync();
        }
        public async Task RoleSeedAsync()
        {
            foreach (var role in Enum.GetValues(typeof(Roles)))
            {
                if (!await _roleManager.RoleExistsAsync(role.ToString()))
                {
                    await _roleManager.CreateAsync(new IdentityRole { Name = role.ToString() });
                }
            }
        }
    }
}
