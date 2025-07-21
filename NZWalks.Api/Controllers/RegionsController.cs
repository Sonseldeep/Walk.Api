using Microsoft.AspNetCore.Mvc;
using NZWalks.Api.Data;

namespace NZWalks.Api.Controllers;


[ApiController]

public class RegionsController : ControllerBase
{
    private readonly ApplicationDbContext _context;
    public RegionsController(ApplicationDbContext context)
    {
        _context = context;
    }
    
    
    [HttpGet("api/regions")]
    public IActionResult GetAll()
    {
        var regions = _context.Regions.ToList();
        return Ok(regions);
    }

    [HttpGet("api/regions/{id:guid}")]
    public IActionResult GetById([FromRoute] Guid id)
    {
        var existingRegion = _context.Regions.SingleOrDefault(x => x.Id == id);
        if (existingRegion is null)
        {
            return NotFound();
        }

        return Ok(existingRegion);
    }
}