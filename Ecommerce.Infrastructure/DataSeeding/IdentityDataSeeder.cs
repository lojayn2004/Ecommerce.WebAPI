using Ecommerce.Domain.Contracts;
using Ecommerce.Domain.Models.Identity;
using Ecommerce.Infrastructure.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;


namespace Ecommerce.Infrastructure.DataSeeding
{
    internal class IdentityDataSeeder(
        StoreIdentityDbContext _dbContext, 
        UserManager<ApplicationUser> _userManager, 
        RoleManager<IdentityRole> _roleManager) : IDataSeeder
    {
        public async Task SeedDataAsync()
        {
            var hasMigratiosn = _dbContext.Database.GetPendingMigrations().Any();
            if (hasMigratiosn)
                await _dbContext.Database.MigrateAsync();
            // Seed Roles
            var hasRoles = await _dbContext.Roles.AnyAsync();
            if (!hasRoles)
            {
                List<string> roles = ["Admin", "SuperAdmin"];

                foreach (var role in roles)
                {
                    bool roleExists = await _roleManager.RoleExistsAsync(role);
                    if (!roleExists)
                    {
                        await _roleManager.CreateAsync(new IdentityRole(role));
                    }
                }
            }

            // Seed Users

            var hasUsers = await _dbContext.Users.AnyAsync();
            if (!hasUsers)
            {

                var superAdminUser = new ApplicationUser
                {
                    DisplayName = "SuperAdmin",
                    UserName = "SuperAdmin",
                    Email = "SuperAdmin@gmail.com"
                };
                var superAdminResult = await _userManager.CreateAsync(superAdminUser, "SuperAdmin@123");
                if (superAdminResult.Succeeded)
                    await _userManager.AddToRoleAsync(superAdminUser, "SuperAdmin");
                // else must log here
            }

        }


    
    

       

    }
}
