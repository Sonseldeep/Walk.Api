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
        return await _dbContext.Walks.ToListAsync();
    }
}