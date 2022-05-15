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

    [HttpGet("equal",Name = "Equal")]
    public Task<IActionResult> EqualAsync([FromQuery]string phone)
    {
        return Task.FromResult<IActionResult>(Ok(_context.Authors.Where(a=>a.Phone == phone).ToList()));
    }
    [HttpGet("contains",Name = "Contains")]
    public Task<IActionResult> ContainsAsync([FromQuery]string phone)
    {
        return Task.FromResult<IActionResult>(Ok(_context.Authors.Where(a=>a.Phone.Contains(phone)).ToList()));
    }

    [HttpPost(Name = "Insert")]
    public async Task<IActionResult> InsertAsync([FromBody]Author author)
    {
        await _context.Authors.AddAsync(author);
        await _context.SaveChangesAsync();
        return NoContent();
    }
}