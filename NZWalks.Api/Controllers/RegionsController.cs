using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NZWalks.Api.CustomActionFilters;
using NZWalks.Api.Data;
using NZWalks.Api.Models.Domain;
using NZWalks.Api.Models.DTO;
using NZWalks.Api.Repositories;
using static NZWalks.Api.Models.Domain.Region;


namespace NZWalks.Api.Controllers;


[ApiController]

public class RegionsController : ControllerBase
{
    private readonly ApplicationDbContext _context;
    private readonly IRegionRepository _regionRepository;
    private readonly IMapper _mapper;

    public RegionsController(ApplicationDbContext context, IRegionRepository regionRepository, IMapper mapper )
    {
        _context = context;
        _regionRepository = regionRepository;
        _mapper = mapper;
    }
    
    
    [HttpGet("api/regions")]
    [ProducesResponseType(StatusCodes.Status200OK)]
 
    public async Task<IActionResult>  GetAll()
    { 
        // 1-> get data from the database - domain model bata
        var regions = await _regionRepository.GetAllAsync();
        
        // 2->  map the domain model to DTOs 
        var regionsDto  = _mapper.Map<List<RegionDto>>(regions);
        
     
       

        // 3->  return DTOS not domain model
        return Ok(regionsDto);
    }
    
    
    [HttpPost("api/regions")]
    [ValidateModel]
    public async Task<IActionResult> Create([FromBody] AddRegionRequestDto addRegionRequestDto )
    {
        
            // Map the DTO to Domain Model
            var regionDomainModel = _mapper.Map<Region>(addRegionRequestDto);

            // Add new region to database
            regionDomainModel = await _regionRepository.CreateAsync(regionDomainModel);
    
         
            // Map the Domain Model to DTO
            var regionDto = _mapper.Map<RegionDto>(regionDomainModel);
         
            return CreatedAtAction(nameof(GetById), new { id = regionDto.Id },regionDto);

            
        
          
        
        
    }

    [HttpPut("api/regions/{id:guid}")]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ValidateModel]
    public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] UpdateRegionRequestDto updateRegionRequestDto)
    {
        
        // Map DTO to Domain Model
        var regionDomainModel = _mapper.Map<Region>(updateRegionRequestDto);
        
        // Check if the region exists
        regionDomainModel = await _regionRepository.UpdateByIdAsync(id, regionDomainModel);
        
        if (regionDomainModel is null)
        {
            return NotFound();
        }
        
        // Convert the Domain Model to DTO
        var regionDto = _mapper.Map<RegionDto>(regionDomainModel);
        return Ok(regionDto);

    }
    
    
    [HttpGet("api/regions/{id:guid}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetById([FromRoute] Guid id)
    {
        var existingRegion = await _regionRepository.GetByIdAsync(id);
        if (existingRegion is null)
        {
            return NotFound();
        }

        var regionDto = _mapper.Map<RegionDto>(existingRegion);
        

        return Ok(regionDto);
    }

  

    [HttpDelete("api/regions/{id:guid}")]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<IActionResult> Delete([FromRoute] Guid id)
    { 
        var regionDomainModel = await _regionRepository.DeleteByIdAsync(id);
        if (regionDomainModel is null)
        {
            return NotFound();
        }
     
        return NoContent();
    }
}