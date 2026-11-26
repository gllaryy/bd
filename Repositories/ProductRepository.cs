using Microsoft.EntityFrameworkCore;
using OrmShop.Data;
using OrmShop.Models;

namespace OrmShop.Repositories;

public class ProductRepository : IProductRepository
{
    private readonly AppDbContext _db;

    public ProductRepository(AppDbContext db)
    {
        _db = db;
    }


    public async Task<List<Product>> GetAllAsync()
    {
        return await _db.Products.Include(p => p.Category).OrderBy(p => p.Id).ToListAsync();
    }

    public async Task<Product?> GetByIdAsync(int id)
    {
        return await _db.Products.Include(p => p.Category).FirstOrDefaultAsync(p => p.Id == id);
    }

    public async Task<Product> CreateAsync(Product product)
    {
        product.Id = 0;                       
        product.CreatedAt = DateTime.UtcNow;
        product.Category = null;
        _db.Products.Add(product);
        await _db.SaveChangesAsync();
        await _db.Entry(product).Reference(p => p.Category).LoadAsync();
        return product;
    }

    public async Task<Product?> UpdateAsync(int id, Product data)
    {
        var existing = await _db.Products.FindAsync(id);
        if (existing is null)
        {
            return null;
        }

        existing.Name = data.Name;
        existing.Price = data.Price;
        existing.Stock = data.Stock;
        existing.CategoryId = data.CategoryId;
        await _db.SaveChangesAsync();
        await _db.Entry(existing).Reference(p => p.Category).LoadAsync();
        return existing;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var existing = await _db.Products.FindAsync(id);
        if (existing is null)
        {
            return false;
        }

        _db.Products.Remove(existing);
        await _db.SaveChangesAsync();
        return true;
    }

    public async Task<bool> CategoryExistsAsync(int categoryId)
    {
        return await _db.Categories.AnyAsync(c => c.Id == categoryId);
    }


    public async Task<List<Product>> GetExpensiveAsync(decimal min)
    {
        return await _db.Products
            .Include(p => p.Category)
            .Where(p => p.Price > min)
            .OrderByDescending(p => p.Price)
            .ToListAsync();
    }

    public async Task<List<Product>> SearchByNameAsync(string name)
    {
        return await _db.Products
            .Include(p => p.Category)
            .Where(p => EF.Functions.ILike(p.Name, "%" + name + "%"))
            .ToListAsync();
    }

    public async Task<List<Product>> GetByCategoryNameAsync(string categoryName)
    {
        return await _db.Products
            .Include(p => p.Category)
            .Where(p => p.Category!.Name == categoryName)
            .OrderBy(p => p.Name)
            .ToListAsync();
    }


    public async Task<List<Product>> RawExpensiveAsync(decimal min)
    {
        return await _db.Products
            .FromSqlInterpolated($"SELECT * FROM products WHERE price > {min}")
            .ToListAsync();
    }
   

    public async Task<List<Product>> RawByNameUnsafeAsync(string name)
    {
        var sql = "SELECT * FROM products WHERE name = '" + name + "'";
        return await _db.Products.FromSqlRaw(sql).ToListAsync();
    }

    public async Task<List<Product>> RawByNameSafeAsync(string name)
    {
        return await _db.Products
            .FromSqlInterpolated($"SELECT * FROM products WHERE name = {name}")
            .ToListAsync();
    }
}