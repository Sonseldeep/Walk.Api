using Microsoft.EntityFrameworkCore;
using NZWalks.Api.Data;
using NZWalks.Api.Models.Domain;

namespace NZWalks.Api.Repositories;

public class SqlWalkRepository: IWalkRepository
{
    private readonly ApplicationDbContext _dbContext;

    // repository maa dbcontext hunxa
    // so ctor garaune
    // ApplicationDbContext data bata inject garne
    public SqlWalkRepository(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    // method ready xa 
    // aabo controller maa use garne
    public async Task<Walk> CreateAsync(Walk walk)
    {
       await  _dbContext.Walks.AddAsync(walk);
       await _dbContext.SaveChangesAsync();
       return walk;
    }


    
    public async Task<List<Walk>> GetAllAsync(string? filterOn = null, string? filterQuery = null, string? sortBy = null, bool isAscending = true)
    {
        // filtering and sorting ko lagi IQueryable use garne
        var walks = _dbContext.Walks
            .AsNoTracking()
            .Include("Difficulty")
            .Include("Region")
            .AsQueryable();

        // Filtering
        if (!string.IsNullOrWhiteSpace(filterOn) && !string.IsNullOrWhiteSpace(filterQuery))
        {
            if (filterOn.Equals("Name", StringComparison.OrdinalIgnoreCase))
            {
                walks = walks.Where(x => x.Name.Contains(filterQuery));
            }
        }

        // Sorting 
        if (!string.IsNullOrWhiteSpace(sortBy))
        {
            if (sortBy.Equals("Name", StringComparison.OrdinalIgnoreCase))
            {
                walks = isAscending ? walks.OrderBy(x => x.Name) : walks.OrderByDescending(x => x.Name);
            }
            // sorting by length
            else if (sortBy.Equals("Length", StringComparison.OrdinalIgnoreCase))
            {
                walks = isAscending ? walks.OrderBy(x => x.LengthInKm) : walks.OrderByDescending(x => x.LengthInKm);
            }
        }

        return await walks.ToListAsync();
    }

    public async Task<Walk?> GetByIdAsync(Guid id)
    {
        var existingWalk = await _dbContext.Walks.Include("Difficulty").Include("Region").SingleOrDefaultAsync(x => x.Id == id);
        return existingWalk;
    }

    public async  Task<Walk?> UpdateAsync(Guid id, Walk walk)
    {
        var existingWalk = await _dbContext.Walks.SingleOrDefaultAsync(x => x.Id == id);
        if (existingWalk is null)
        {
            return null;
        }

        existingWalk.Name = walk.Name;
        existingWalk.Description = walk.Description;
        existingWalk.LengthInKm = walk.LengthInKm;
        existingWalk.WalkImageUrl = walk.WalkImageUrl;
        existingWalk.RegionId = walk.RegionId;
        existingWalk.DifficultyId = walk.DifficultyId;

        await _dbContext.SaveChangesAsync();
        return existingWalk;

    }

    public async Task<Walk?> DeleteByIdAsync(Guid id)
    {
       var existingWalk =  await _dbContext.Walks.SingleOrDefaultAsync(x=> x.Id == id);
       if (existingWalk is null)
       {
           return null;
       }
       _dbContext.Walks.Remove(existingWalk);
       await _dbContext.SaveChangesAsync();
       return existingWalk; 
    }
}