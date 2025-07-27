using Microsoft.EntityFrameworkCore;
using NZWalks.Api.Data;
using NZWalks.Api.Models.Domain;

namespace NZWalks.Api.Repositories;

public class SqlRegionRepository : IRegionRepository
{
    private readonly ApplicationDbContext _context;
    public SqlRegionRepository(ApplicationDbContext context)
    {
        _context = context;

    }
    public async Task<List<Region>> GetAllAsync()
    {
       return  await _context.Regions.ToListAsync();
    }

    public async Task<Region?> GetByIdAsync(Guid id)
    {
       return await _context
           .Regions
           .SingleOrDefaultAsync(x => x.Id == id);
    }

    public async Task<Region> CreateAsync(Region region)
    {
        await _context.Regions.AddAsync(region);
        await _context.SaveChangesAsync();
        return region;
    }

    public async Task<Region?> UpdateByIdAsync(Guid id, Region region)
    {
        var existingRegion = await _context.Regions.SingleOrDefaultAsync(x => x.Id == id);
        if (existingRegion is null)
        {
            return null;
        }
        existingRegion.Code = region.Code;
        existingRegion.Name = region.Name;
        existingRegion.RegionImageUrl = region.RegionImageUrl;
         
        await _context.SaveChangesAsync();
        return existingRegion;
    }

    public async Task<Region?> DeleteBYIdAsync(Guid id)
    {
        var existingRegion = await _context.Regions.SingleOrDefaultAsync(x => x.Id == id);
        if (existingRegion is null)
        {
            return null;
        }
        _context.Regions.Remove(existingRegion);
        await _context.SaveChangesAsync();
        return existingRegion;
    }

}