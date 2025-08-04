using NZWalks.Api.Models.Domain;

namespace NZWalks.Api.Repositories;

public interface IWalkRepository
{
    // Create garauda Walk domain model cahinxa
    // task ko type walk ho 
   Task<Walk> CreateAsync(Walk walk);
     
   // GetALl ko pani definition garaune
   // filter garda controller maa ke xa tei name yeta ni garako suru maa null hunxa default
   // pagination ko lagi pageNumber and pageSize ni add garne

   Task<List<Walk>> GetAllAsync(string? filterOn = null, string? filterQuery = null, string? sortBy = null, bool isAscending  = true, int pageNumber = 1, int pageSize = 1000 );
   
   // Get BY ID 
   Task<Walk?> GetByIdAsync(Guid id);
   
   // Update walk ko logic databse
   Task<Walk?> UpdateAsync(Guid id, Walk walk);
   
   // Delete walk by id
   Task<Walk?> DeleteByIdAsync(Guid id);
}