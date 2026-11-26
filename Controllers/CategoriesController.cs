using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OrmShop.Data;
using OrmShop.Models;

namespace OrmShop.Controllers;

[ApiController]
[Route("categories")]
public class CategoriesController : ControllerBase
{
    private readonly AppDbContext _db;

    public CategoriesController(AppDbContext db)
    {
        _db = db;
    }

    [HttpGet]
    public async Task<ActionResult<List<Category>>> GetAll()
    {
        return Ok(await _db.Categories.OrderBy(c => c.Id).ToListAsync());
    }
}