using OrmShop.Models;

namespace OrmShop.Data;

public static class DatabaseSeeder
{
    public static void SeedTestData(AppDbContext context)
    {
        if (context.Categories.Any())
        {
            return;
        }

        var electronics = new Category { Name = "Electronics" };
        var clothing = new Category { Name = "Clothing" };
        var books = new Category { Name = "Books" };
        context.Categories.AddRange(electronics, clothing, books);

        context.Products.AddRange(
            new Product { Name = "Smartphone", Price = 12000.00m, Stock = 10, Category = electronics },
            new Product { Name = "Headphones", Price = 1500.00m, Stock = 25, Category = electronics },
            new Product { Name = "USB Cable", Price = 90.00m, Stock = 100, Category = electronics },
            new Product { Name = "Jacket", Price = 3200.00m, Stock = 8, Category = clothing },
            new Product { Name = "T-shirt", Price = 450.00m, Stock = 40, Category = clothing },
            new Product { Name = "SQL Textbook", Price = 380.00m, Stock = 15, Category = books }
        );

        context.SaveChanges();
        Console.WriteLine("Seed: test data added");
    }
}