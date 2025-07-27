using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
 
    public async Task<IActionResult>  GetAll()
    { 
        // 1-> get data from the database - domain model bata
        var regions = await _context.Regions.ToListAsync();
        
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
    public async Task<IActionResult> Create([FromBody] AddRegionRequestDto addRegionRequestDto )
    {
        // Map the DTO to Domain Model
        var regionDomainModel = new Region
        {

            Code = addRegionRequestDto.Code,
            Name = addRegionRequestDto.Name,
            RegionImageUrl = addRegionRequestDto.RegionImageUrl
          
        };

        // Add new region to database
        await _context.Regions.AddAsync(regionDomainModel);
        await _context.SaveChangesAsync();
         
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

    [HttpPut("api/regions/{id:guid}")]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] UpdateRegionRequestDto updateRegionRequestDto)
    {
        // Check if the region exists
        var regionDomainModel =await _context.Regions.SingleOrDefaultAsync(region => region.Id == id);
        
        if (regionDomainModel is null)
        {
            return NotFound();
        }
        
        // Map the DTO to Domain Model
        regionDomainModel.Code = updateRegionRequestDto.Code;
        regionDomainModel.Name = updateRegionRequestDto.Name;
        regionDomainModel.RegionImageUrl = updateRegionRequestDto.RegionImageUrl;
        
        // Update the region in the database
        await _context.SaveChangesAsync();
        
        // Convert the Domain Model to DTO
        var regionDto = new RegionDto
        {
            Id = regionDomainModel.Id,
            Code = regionDomainModel.Code,
            Name = regionDomainModel.Name,
            RegionImageUrl = regionDomainModel.RegionImageUrl
        };
 
        return Ok(regionDto);
    }
    
    
    [HttpGet("api/regions/{id:guid}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetById([FromRoute] Guid id)
    {
        var existingRegion = await _context.Regions.SingleOrDefaultAsync(x => x.Id == id);
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
        

        return Ok(regionDto);
    }

  

    [HttpDelete("api/regions/{id:guid}")]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<IActionResult> Delete([FromRoute] Guid id)
    {
        var existingRegion =await _context.Regions.SingleOrDefaultAsync(x => x.Id == id);
        if (existingRegion is null)
        {
            return NotFound();
        }
        _context.Regions.Remove(existingRegion);
        await _context.SaveChangesAsync();

        return NoContent();
    }
}