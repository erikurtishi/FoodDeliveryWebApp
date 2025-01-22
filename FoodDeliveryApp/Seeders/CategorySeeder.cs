using FoodDeliveryApp.Data;
using FoodDeliveryApp.Models;
using Microsoft.EntityFrameworkCore;

namespace FoodDeliveryApp.Seeders;

public class CategorySeeder
{
    public static async Task SeedCategoriesAsync(AppDbContext context)
    {
        // Check if the Categories table already has data
        if (!await context.Categories.AnyAsync())
        {
            // Define a list of categories to seed
            var categories = new List<Category>
            {
                new Category { Name = "Breakfast", Description = "Start your day with a delicious breakfast." },
                new Category { Name = "Soups", Description = "Warm and comforting soup options." },
                new Category { Name = "Salads", Description = "Fresh and healthy salad options." },
                new Category { Name = "Sandwiches", Description = "Quick and tasty sandwich selections." },
                new Category { Name = "Burgers", Description = "Juicy and delicious burgers for all tastes." },
                new Category { Name = "Pizzas", Description = "Authentic pizzas with various toppings." },
                new Category { Name = "Pasta", Description = "Classic pasta dishes from Italy." },
                new Category { Name = "Meats", Description = "Grilled and roasted meat dishes." },
                new Category { Name = "SeaFood", Description = "Fresh seafood delicacies." },
                new Category { Name = "Desserts", Description = "Sweet treats to end your meal." },
                new Category { Name = "Drinks", Description = "Refreshing beverages and cocktails." }
            };

            // Add categories to the database
            await context.Categories.AddRangeAsync(categories);
            await context.SaveChangesAsync();
        }
    }
}
