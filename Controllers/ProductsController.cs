using Microsoft.AspNetCore.Mvc;
using OrmShop.Models;
using OrmShop.Repositories;

namespace OrmShop.Controllers;

[ApiController]
[Route("products")]
public class ProductsController : ControllerBase
{
    private readonly IProductRepository _repo;

    public ProductsController(IProductRepository repo)
    {
        _repo = repo;
    }

    [HttpGet]
    public async Task<ActionResult<List<Product>>> GetAll()
    {
        return Ok(await _repo.GetAllAsync());
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<Product>> GetOne(int id)
    {
        var product = await _repo.GetByIdAsync(id);
        if (product is null)
        {
            return NotFound();
        }
        return Ok(product);
    }

    [HttpPost]
    public async Task<ActionResult<Product>> Create([FromBody] Product product)
    {
        if (!await _repo.CategoryExistsAsync(product.CategoryId))
        {
            return BadRequest($"Category not found: {product.CategoryId}");
        }

        var created = await _repo.CreateAsync(product);
        return CreatedAtAction(nameof(GetOne), new { id = created.Id }, created);
    }

    [HttpPut("{id:int}")]
    public async Task<ActionResult<Product>> Update(int id, [FromBody] Product product)
    {
        if (!await _repo.CategoryExistsAsync(product.CategoryId))
        {
            return BadRequest($"Category not found: {product.CategoryId}");
        }

        var updated = await _repo.UpdateAsync(id, product);
        if (updated is null)
        {
            return NotFound();
        }
        return Ok(updated);
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        if (!await _repo.DeleteAsync(id))
        {
            return NotFound();
        }
        return NoContent();
    }
}