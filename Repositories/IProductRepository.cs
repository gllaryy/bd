using OrmShop.Models;

namespace OrmShop.Repositories;

public interface IProductRepository
{
    Task<List<Product>> GetAllAsync();
    Task<Product?> GetByIdAsync(int id);
    Task<Product> CreateAsync(Product product);
    Task<Product?> UpdateAsync(int id, Product data);
    Task<bool> DeleteAsync(int id);
    Task<bool> CategoryExistsAsync(int categoryId);

    
    Task<List<Product>> GetExpensiveAsync(decimal min);
    Task<List<Product>> SearchByNameAsync(string name);
    Task<List<Product>> GetByCategoryNameAsync(string categoryName);

  
    Task<List<Product>> RawExpensiveAsync(decimal min);
    Task<List<Product>> RawByNameUnsafeAsync(string name);
    Task<List<Product>> RawByNameSafeAsync(string name);
}