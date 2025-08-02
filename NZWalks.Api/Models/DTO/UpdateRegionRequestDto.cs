using System.ComponentModel.DataAnnotations;

namespace NZWalks.Api.Models.DTO;

public class UpdateRegionRequestDto
{
    [Required]
    [MinLength(3, ErrorMessage = "Code must be at least 3 characters long.")]
    [MaxLength(3, ErrorMessage = "Code must be exactly 3 characters long.")]
    public string Code { get; set; } = string.Empty;

    [Required]
    [MaxLength(100, ErrorMessage = "Name cannot exceed 100 characters.")]
    public string Name { get; set; } = string.Empty;

    public string? RegionImageUrl { get; set; } = string.Empty;
}