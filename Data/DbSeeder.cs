using Microsoft.AspNetCore.Identity;
using SportSync.Models;

namespace SportSync.Data
{
    public static class DbSeeder
    {
        public static async Task SeedUsers(UserManager<User> userManager, RoleManager<IdentityRole> roleManager)
        {
            // Création du rôle Admin s'il n'existe pas
            if (!await roleManager.RoleExistsAsync("Admin"))
            {
                await roleManager.CreateAsync(new IdentityRole("Admin"));
            }

            // Création du rôle User s'il n'existe pas
            if (!await roleManager.RoleExistsAsync("User"))
            {
                await roleManager.CreateAsync(new IdentityRole("User"));
            }

            // Création du compte admin
            if (await userManager.FindByEmailAsync("admin@sportsync.com") == null)
            {
                var adminUser = new User
                {
                    UserName = "admin@sportsync.com",
                    Email = "admin@sportsync.com",
                    EmailConfirmed = true,
                    FirstName = "Admin",
                    LastName = "User",
                    Gender = "Other",
                    PhoneNumber = "0000000000",
                    IsActive = true,
                    CreatedAt = DateTime.UtcNow
                };

                var result = await userManager.CreateAsync(adminUser, "Admin123!");
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(adminUser, "Admin");
                }
            }

            // Création du compte utilisateur test
            if (await userManager.FindByEmailAsync("aya.dyr@example.com") == null)
            {
                var testUser = new User
                {
                    UserName = "aya.dyr@example.com",
                    Email = "aya.dyr@example.com",
                    EmailConfirmed = true,
                    FirstName = "Aya",
                    LastName = "Dyr",
                    Gender = "Female",
                    PhoneNumber = "0000000000",
                    IsActive = true,
                    CreatedAt = DateTime.UtcNow
                };

                var result = await userManager.CreateAsync(testUser, "Test123!");
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(testUser, "User");
                }
            }
        }
    }
}
