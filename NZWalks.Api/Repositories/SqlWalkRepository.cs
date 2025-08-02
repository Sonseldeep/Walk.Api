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

    public async Task<List<Walk>> GetAllAsync()
    {
        return await _dbContext.Walks.Include("Difficulty").Include("Region").ToListAsync();
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