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

    // filter garisake paxi, controller ra IWalkRepository maa
    // GetAllAsync maa pani filterOn and filterQuery lai add agre as parameters
    // so that we can filter the data based on the query
    public async Task<List<Walk>> GetAllAsync(string? filterOn = null, string? filterQuery = null)
    {
        // Note : Sorting, filtering, pagination gardaaa we have to use IQueryable
        // so that we can apply the filter on the IQueryable and then execute the query
        var walks = _dbContext.Walks.AsNoTracking().Include("Difficulty").Include("Region").AsQueryable();
        
        // Filtering
        if (string.IsNullOrWhiteSpace(filterOn) != false || string.IsNullOrWhiteSpace(filterQuery) != false)
            return await walks.ToListAsync();
        if (filterOn.Equals("Name", StringComparison.OrdinalIgnoreCase))
        {
            walks = walks.Where(x => x.Name.Contains(filterQuery));
        }

        return await walks.ToListAsync();
        // return await _dbContext.Walks.Include("Difficulty").Include("Regio n").ToListAsync();
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