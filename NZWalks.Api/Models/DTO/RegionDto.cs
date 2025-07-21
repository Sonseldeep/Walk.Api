namespace NZWalks.Api.Models.DTO;

public class RegionDto
{
    public required Guid Id { get; set; }
    public required string Code { get; set; } =string.Empty;
    public required string Name { get; set; } = string.Empty;
    public string? RegionImageUrl { get; set; } = string.Empty;
}