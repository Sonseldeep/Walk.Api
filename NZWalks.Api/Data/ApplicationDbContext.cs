using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NZWalks.Api.Models.Domain;

namespace NZWalks.Api.Data;

public class ApplicationDbContext: DbContext
{
  public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
  {
    
  }

  public DbSet<Difficulty> Difficulties { get; set; }
  public DbSet<Region> Regions { get; set; }
  public DbSet<Walk> Walks { get; set; }

  protected override void OnModelCreating(ModelBuilder modelBuilder)
  {
   base.OnModelCreating(modelBuilder);
   // seed data for difficulties
   // Easy, Medium, Hard
   var difficulties = new List<Difficulty>
   {
       new Difficulty() { Id = Guid.Parse("0F979D1A-5F56-4C03-8AC8-5EA842A40364"), Name = "Easy" },
       new Difficulty() { Id = Guid.Parse("C57640EF-B130-49BF-AB1C-C7F820FE6725"), Name = "Medium" },
       new Difficulty() { Id = Guid.Parse("FDCF6BC0-D118-4BE7-9C0D-71A03AF164DB"), Name = "Hard" }
   };

   // Add the difficulties to the database
   modelBuilder.Entity<Difficulty>().HasData(difficulties);

  }
}