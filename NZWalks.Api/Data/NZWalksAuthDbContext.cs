using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace NZWalks.Api.Data;

public class NZWalksAuthDbContext : IdentityDbContext
{
    public NZWalksAuthDbContext(DbContextOptions<NZWalksAuthDbContext> options) : base(options)
    {
        
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        // Seed data for roles
      
        const string readerRoleId = "C9D8BFE6-8649-4292-942E-1001CE7934F7";
        const string writerRoleId = "051E03D2-7958-41F0-8901-1E0D3A287208";
        // IdentityRole chai microsoft ko ho include garne
        var roles = new List<IdentityRole>
        {
            // fisrt role garaune
            new IdentityRole
            {
                Id = readerRoleId,
                ConcurrencyStamp = readerRoleId,
                Name = "Reader",
                NormalizedName = "READER".ToUpper()
            },
            new  IdentityRole
            {
                Id = writerRoleId,
                ConcurrencyStamp = writerRoleId,
                Name = "Writer",
                NormalizedName = "WRITER".ToUpper()
            }
        };
        builder.Entity<IdentityRole>().HasData(roles);

    }
} 