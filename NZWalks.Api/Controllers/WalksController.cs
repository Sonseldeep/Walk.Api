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
}