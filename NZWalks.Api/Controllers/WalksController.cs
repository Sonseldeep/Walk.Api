using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using NZWalks.Api.Models.Domain;
using NZWalks.Api.Models.DTO;
using NZWalks.Api.Repositories;

namespace NZWalks.Api.Controllers;
[ApiController]
[Route("api")]
public class WalksController : ControllerBase
{
    private readonly IMapper _mapper;
    private readonly IWalkRepository _walkRepository;

    //mapping ko lagi ctro garaune
    // mapper maa ctr . gareyo vane aafai hunxa 
    // also IWaklRepository pani inject garne
    public WalksController(IMapper mapper, IWalkRepository  walkRepository)
    {
        _mapper = mapper;
        _walkRepository = walkRepository;
    }
    
    // create walk
    //post: /api/walks
    [HttpPost("/walks")]
    public async Task<IActionResult> Create([FromBody] AddWalkRequestDto addWalkRequestDto)
    {
        // Map AddWalkRequestDto to Domain model
       var walkDomainModel =  _mapper.Map<Walk>(addWalkRequestDto);
       await _walkRepository.CreateAsync(walkDomainModel);
       // Map Domain Model to DTO
       return Ok(    _mapper.Map<WalkDto>(walkDomainModel)
       );
        
        
    }


    [HttpGet("walks")]
    public async Task<IActionResult> GetAll()
    {
        var walksDomainModel =  await _walkRepository.GetAllAsync();
        // Map Domain Model to DTO
        return Ok(_mapper.Map<List<WalkDto>>(walksDomainModel));
    }
    
    // Get By Id

    [HttpGet("walks/{id:guid}")]
    public async Task<IActionResult> GetById([FromRoute] Guid id)
    {
        var walkDomainModel = await _walkRepository.GetByIdAsync(id);
        if (walkDomainModel is null)
        {
            return NotFound();
        }
        // Map Domain Model to DTO
        return Ok(_mapper.Map<WalkDto>(walkDomainModel));
    }
    
    // update walk
    [HttpPut("walks/{id:guid}")]
    public async Task<IActionResult> Update([FromRoute] Guid id, UpdateWalkRequestDto updateWalkRequestDto)
    {
        // Map DTO to Domain Model
        var walkDomainModel = _mapper.Map<Walk>(updateWalkRequestDto);
        walkDomainModel = await _walkRepository.UpdateAsync(id, walkDomainModel);
        
        if( walkDomainModel is null)
        {
            return NotFound();
        }
        
        // Map Domain Model to DTO
        return Ok(_mapper.Map<WalkDto>(walkDomainModel));
    }
    
    // Delete Walk

    [HttpDelete("walks/{id:guid}")]
    public async Task<IActionResult> Delete([FromRoute] Guid id)
    {
        var deleteDomainModel = await _walkRepository.DeleteByIdAsync(id);
        if (deleteDomainModel is null)
        {
            return NotFound(); 
        }

        return NoContent();
    }
}