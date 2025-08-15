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
  public DbSet<Walk>  Walks { get; set; }
  public DbSet<Image> Images { get; set; }

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
   
   // Seed data for regions
   var regions = new List<Region>
   {
       new Region
       {
           Id = Guid.Parse("CDDE7BD2-DE9A-47B3-AC79-42190D98E619"),
           Code = "AKL",
           Name = "Auckland",
           RegionImageUrl = "https://images.pexels.com/photos/17824133/pexels-photo-17824133.jpeg"
       },
       new Region
       {
           Id = Guid.Parse("B1F0A2D3-4E6C-4B5A-8F7B-9C8D1E2F3A4B"),
           Code = "WLG",
           Name = "Wellington",
           RegionImageUrl = "https://images.pexels.com/photos/395939/pexels-photo-395939.jpeg"
       },
       new Region
       {
           Id = Guid.Parse("A2B1C3D4-E5F6-4A7B-8C9D-0E1F2A3B4C5D"),
           Code = "CHC",
           Name = "Christchurch",
           RegionImageUrl = "https://images.pexels.com/photos/27912270/pexels-photo-27912270.jpeg"
       },
       new Region
       {
           Id = Guid.Parse("F1E2D3C4-B5A6-47B8-9C0D-1E2F3A4B5C6D"),
           Code = "DUD",
           Name = "Dunedin",
           RegionImageUrl = "https://images.pexels.com/photos/424611/pexels-photo-424611.jpeg"
       },
       new Region
       {
           Id = Guid.Parse("12345678-90AB-CDEF-1234-567890ABCDEF"),
           Code = "NSN",
           Name = "Nelson",
           RegionImageUrl = "https://images.pexels.com/photos/3396651/pexels-photo-3396651.jpeg"
       },
       new Region
       {
           Id = Guid.Parse("ABCDEF12-3456-7890-ABCD-EF1234567890"),
           Code = "TGA",
           Name = "Tauranga",
           RegionImageUrl = "https://images.pexels.com/photos/17982626/pexels-photo-17982626.jpeg"
       },
       new Region
       {
           Id = Guid.Parse("FEDCBA98-7654-3210-FEDC-BA9876543210"),
           Code = "NPE",
           Name = "Napier",
           RegionImageUrl = "https://images.pexels.com/photos/20326273/pexels-photo-20326273.jpeg"
       }
   };
   
   modelBuilder.Entity<Region>().HasData(regions);
  }
}