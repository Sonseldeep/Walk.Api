namespace NZWalks.Api.Models.DTO;

public class WalkDto
{
    public required Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public double LengthInKm { get; set; }
    public string? WalkImageUrl { get; set; } = string.Empty;
  


    public RegionDto Region { get; set; }
    public DifficultyDto Difficulty { get; set; }
     
}