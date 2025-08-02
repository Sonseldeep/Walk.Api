using System.ComponentModel.DataAnnotations;

namespace NZWalks.Api.Models.DTO;

public class AddRegionRequestDto
{
    [MinLength(3,ErrorMessage = "Region code must be at least 3 characters long.")]
    [MaxLength(3, ErrorMessage = "Region code must be exactly 3 characters long.")]
    public required string Code { get; set; } =string.Empty;
    
    [MaxLength(100, ErrorMessage = "Region name cannot exceed 100 characters.")]
    public required string Name { get; set; } = string.Empty;
    public string? RegionImageUrl { get; set; } = string.Empty;
}