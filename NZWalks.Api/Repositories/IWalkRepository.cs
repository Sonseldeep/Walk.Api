using NZWalks.Api.Models.Domain;

namespace NZWalks.Api.Repositories;

public interface IWalkRepository
{
    // Create garauda Walk domain model cahinxa
    // task ko type walk ho 
   Task<Walk> CreateAsync(Walk walk);
     
   // GetALl ko pani definition garaune

   Task<List<Walk>> GetAllAsync();
   
   // Get BY ID 
   Task<Walk?> GetByIdAsync(Guid id);
   
   // Update walk ko logic databse
   Task<Walk?> UpdateAsync(Guid id, Walk walk);
   
   // Delete walk by id
   Task<Walk?> DeleteByIdAsync(Guid id);
}