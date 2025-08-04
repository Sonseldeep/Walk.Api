using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using NZWalks.Api.CustomActionFilters;
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
    [ValidateModel]
    public async Task<IActionResult> Create([FromBody] AddWalkRequestDto addWalkRequestDto)
    {
  

        // Map AddWalkRequestDto to Domain model
        var walkDomainModel =  _mapper.Map<Walk>(addWalkRequestDto);
        await _walkRepository.CreateAsync(walkDomainModel);
        // Map Domain Model to DTO
        return Ok(    _mapper.Map<WalkDto>(walkDomainModel)
        );



    }


    // get all maa filter laune ho
    // /api/walks?filterOn=Name&filterQuery=Track
    // filter garna 2 taa filtrOn, filterQuery para hunxa
    // Sorting ko lagi, next arko parameter pathaune
    // /api/walks?filterOn=Name&filterQuery=Track&sortBy=Name&isAscending=true
    
    
    // Pagination : 
    // /api/walks?filterOn=Name&filterQuery=Track&sortBy=Name&isAscending=true&pageNumber=1pageSize=10
    
    // after that IWalkRepo maa ni add garnu parxa
    [HttpGet("walks")]
    public async Task<IActionResult> GetAll([FromQuery] string? filterOn, [FromQuery] string? filterQuery,[FromQuery] string? sortBy,[FromQuery] bool? isAscending, [FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 1000)
    {
        // not to forget to pass the sortBy and isAscending to the parameters below 
        // isAscending is nullable, so ?? true garnu parxa 
        
        var walksDomainModel =  await _walkRepository.GetAllAsync(filterOn,filterQuery,sortBy,
            isAscending ?? true, pageNumber, pageSize);
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
    [ValidateModel]
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