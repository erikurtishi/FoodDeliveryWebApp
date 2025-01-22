using FoodDeliveryApp.Models;
using Microsoft.AspNetCore.Identity;

namespace FoodDeliveryApp.Seeders;

public class UserSeeder
{
    public static async Task SeedAsync(IServiceProvider serviceProvider)
    {
        var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
        var userManager = serviceProvider.GetRequiredService<UserManager<AppUser>>();
        var roles = new[] { "Admin", "BasicUser", "Restaurant", "Driver" };

        foreach (var role in roles)
        {
            if (!await roleManager.RoleExistsAsync(role))
            {
                await roleManager.CreateAsync(new IdentityRole(role));
            }
        }
        await CreateUser(userManager, "admin@admin.com", "Admin@123", "Admin", "Admin User", "Admin Address", 35, "1234567890");

        await CreateUser(userManager, "user1@example.com", "User@123", "BasicUser", "User 1", "Basic Address 1", 20, "1111111111");
        await CreateUser(userManager, "user2@example.com", "User@123", "BasicUser", "User 2", "Basic Address 2", 21, "1111111112");
        await CreateUser(userManager, "user3@example.com", "User@123", "BasicUser", "User 3", "Basic Address 3", 22, "1111111113");

        await CreateUser(userManager, "example1@restaurant.com", "Restaurant@123", "Restaurant", "Restaurant 1", "Restaurant Address 1", 30, "2222222221");
        await CreateUser(userManager, "example2@restaurant.com", "Restaurant@123", "Restaurant", "Restaurant 2", "Restaurant Address 2", 31, "2222222222");
        await CreateUser(userManager, "example3@restaurant.com", "Restaurant@123", "Restaurant", "Restaurant 3", "Restaurant Address 3", 32, "2222222223");

        await CreateUser(userManager, "example1@driver.com", "Driver@123", "Driver", "Driver 1", "Driver Address 1", 25, "3333333331");
        await CreateUser(userManager, "example2@driver.com", "Driver@123", "Driver", "Driver 2", "Driver Address 2", 26, "3333333332");
        await CreateUser(userManager, "example3@driver.com", "Driver@123", "Driver", "Driver 3", "Driver Address 3", 27, "3333333333");
    }

    private static async Task CreateUser(UserManager<AppUser> userManager, string email, string password, string role, string fullName, string address, int age, string phoneNumber)
    {
        if (await userManager.FindByEmailAsync(email) == null)
        {
            var user = new AppUser
            {
                UserName = email,
                Email = email,
                FullName = fullName,
                Address = address,
                Age = age,
                PhoneNumber = phoneNumber
            };
            var result = await userManager.CreateAsync(user, password);
            if (result.Succeeded)
            {
                await userManager.AddToRoleAsync(user, role);
            }
        }
    }
}