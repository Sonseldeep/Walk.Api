using NZWalks.Api.Models.Domain;

namespace NZWalks.Api.Repositories;

public interface IRegionRepository
{
    Task<List<Region>> GetAllAsync();
    Task<Region?> GetByIdAsync(Guid id);

    Task<Region> CreateAsync(Region region);

    Task<Region?> UpdateByIdAsync(Guid id, Region region);
    Task<Region?> DeleteByIdAsync(Guid id);
}