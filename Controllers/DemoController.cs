using Microsoft.AspNetCore.Mvc;
using OrmShop.Models;
using OrmShop.Repositories;

namespace OrmShop.Controllers;

[ApiController]
[Route("demo")]
public class DemoController : ControllerBase
{
    private readonly IProductRepository _repo;

    public DemoController(IProductRepository repo)
    {
        _repo = repo;
    }


    [HttpGet("raw/expensive")]
    public async Task<ActionResult<List<Product>>> RawExpensive([FromQuery] decimal min = 100)
    {
        return Ok(await _repo.RawExpensiveAsync(min));
    }

    [HttpGet("raw/unsafe")]
    public async Task<ActionResult<List<Product>>> RawUnsafe([FromQuery] string name)
    {
        return Ok(await _repo.RawByNameUnsafeAsync(name));
    }

    [HttpGet("raw/safe")]
    public async Task<ActionResult<List<Product>>> RawSafe([FromQuery] string name)
    {
        return Ok(await _repo.RawByNameSafeAsync(name));
    }


    [HttpGet("orm/expensive")]
    public async Task<ActionResult<List<Product>>> OrmExpensive([FromQuery] decimal min = 100)
    {
        return Ok(await _repo.GetExpensiveAsync(min));
    }

    [HttpGet("orm/search")]
    public async Task<ActionResult<List<Product>>> OrmSearch([FromQuery] string name)
    {
        return Ok(await _repo.SearchByNameAsync(name));
    }

    [HttpGet("orm/by-category")]
    public async Task<ActionResult<List<Product>>> OrmByCategory([FromQuery] string name)
    {
        return Ok(await _repo.GetByCategoryNameAsync(name));
    }
}