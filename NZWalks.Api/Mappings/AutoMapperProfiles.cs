using AutoMapper;
using NZWalks.Api.Models.Domain;
using NZWalks.Api.Models.DTO;

namespace NZWalks.Api.Mappings;

public class AutoMapperProfiles : Profile
{
    public AutoMapperProfiles()
    {
        CreateMap<Region, RegionDto>().ReverseMap();
    }
}