using EntityFrameworkCore.DataEncryption.Sample.Context;
using EntityFrameworkCore.DataEncryption.Sample.Entities;
using Microsoft.AspNetCore.Mvc;

namespace EntityFrameworkCore.DataEncryption.Sample.Controllers;

[ApiController]
[Route("[controller]")]
public class AuthorController : ControllerBase
{
    private readonly SampleDbContext _context;

    public AuthorController(SampleDbContext context)
    {
        _context = context;
    }

    [HttpGet(Name = "Filter")]
    public Task<IActionResult> FilterAsync([FromQuery]string phone)
    {
        return Task.FromResult<IActionResult>(Ok(_context.Authors.Where(a=>a.Phone == phone).ToList()));
    }

    [HttpPost(Name = "Insert")]
    public async Task<IActionResult> InsertAsync([FromBody]Author author)
    {
        await _context.Authors.AddAsync(author);
        await _context.SaveChangesAsync();
        return NoContent();
    }
}