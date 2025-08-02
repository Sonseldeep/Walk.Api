namespace NZWalks.Api.Models.DTO;

public class UpdateWalkRequestDto
{
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public double LengthInKm { get; set; }
    public string? WalkImageUrl { get; set; } = string.Empty;
  
    public Guid DifficultyId { get; set; }
    public Guid RegionId { get; set; }
} 