using Microsoft.AspNetCore.Mvc;
using NZWalks.Api.Data;
using NZWalks.Api.Models.Domain;

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
    [ProducesResponseType(StatusCodes.Status200OK)]
 
    public IActionResult GetAll()
    {
        var regions = _context.Regions.ToList();
        return Ok(regions);
    }

    [HttpGet("api/regions/{id:guid}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public IActionResult GetById([FromRoute] Guid id)
    {
        var existingRegion = _context.Regions.SingleOrDefault(x => x.Id == id);
        if (existingRegion is null)
        {
            return NotFound();
        }

        return Ok(existingRegion);
    }

    [HttpDelete("api/regions/{id:guid}")]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public IActionResult Delete([FromRoute] Guid id)
    {
        var existingRegion = _context.Regions.SingleOrDefault(x => x.Id == id);
        if (existingRegion is null)
        {
            return NotFound();
        }
        _context.Regions.Remove(existingRegion);
        _context.SaveChanges();

        return NoContent();
    }
}