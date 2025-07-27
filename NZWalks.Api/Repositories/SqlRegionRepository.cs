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
}