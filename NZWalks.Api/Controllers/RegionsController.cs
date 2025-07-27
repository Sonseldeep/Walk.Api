using Microsoft.AspNetCore.Mvc;
using NZWalks.Api.Data;
using NZWalks.Api.Models.Domain;
using NZWalks.Api.Models.DTO;
using static NZWalks.Api.Models.Domain.Region;


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
        // 1-> get data from the database - domain model bata
        var regions = _context.Regions.ToList();
        
        // 2->  map the domain model to DTOs 
        var regionsDto = regions.Select(regionDomain => new RegionDto()
        {
            Id = regionDomain.Id,
            Name = regionDomain.Name,
            Code = regionDomain.Code,
            RegionImageUrl = regionDomain.RegionImageUrl
        }).ToList();
        
       

        // 3->  return DTOS not domain model
        return Ok(regionsDto);
    }
    [HttpPost("api/regions")]
    public IActionResult Create([FromBody] AddRegionRequestDto addRegionRequestDto )
    {
        // Map the DTO to Domain Model
        var regionDomainModel = new Region
        {

            Code = addRegionRequestDto.Code,
            Name = addRegionRequestDto.Name,
            RegionImageUrl = addRegionRequestDto.RegionImageUrl
          
        };

        // Add new region to database
        _context.Regions.Add(regionDomainModel);
        _context.SaveChanges();
         
        // Map the Domain Model to DTO
        var regionDto = new RegionDto
        {
            Id = regionDomainModel.Id,
            Code = regionDomainModel.Code,
            Name = regionDomainModel.Name,
            RegionImageUrl = regionDomainModel.RegionImageUrl

        };
         
        return CreatedAtAction(nameof(GetById), new { id = regionDto.Id },regionDto);
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

        var regionDto = new RegionDto()
        {
            Id = existingRegion.Id,
            Name = existingRegion.Name,
            Code = existingRegion.Code,
            RegionImageUrl = existingRegion.RegionImageUrl

        };
        

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