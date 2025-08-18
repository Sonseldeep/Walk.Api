using Microsoft.AspNetCore.Mvc;
using NZWalks.Api.Models.Domain;
using NZWalks.Api.Models.DTO;
using NZWalks.Api.Repositories;

namespace NZWalks.Api.Controllers;

[ApiController]
public class ImagesController : ControllerBase
{
    private readonly IImageRepository _imageRepository;

    public ImagesController(IImageRepository imageRepository)
    {
        _imageRepository = imageRepository;
    }
    
    
    
    // POST: api/images/upload
    [HttpPost("api/images/upload")]
    public async Task<IActionResult> Upload([FromForm] ImageUploadRequestDto request)
    {
        ValidateFileUpload(request);
        if (ModelState.IsValid)
        {
            // convert DTO to Domain Model
            var imageDomainModel = new Image()
            {
                File = request.File,
                FileExtension = Path.GetExtension(request.FileName),
                FileSizeInBytes = request.File.Length,
                FileName = request.FileName,
                FileDescription = request.FileDescription,

            };
            await _imageRepository.Upload(imageDomainModel);
            return Ok(imageDomainModel);
        }
        return BadRequest(ModelState);
    }
    
    // Image validate garna ko lagi format ra size check garne

    private void ValidateFileUpload(ImageUploadRequestDto request)
    {
        var allowedExtensions = new string[] {".jpg", ".jpeg", ".png"};
        if (!allowedExtensions.Contains(Path.GetExtension(request.File.FileName)))
        {
            ModelState.AddModelError("file", "Unsupported file extension. Allowed extensions are: " );
        }

        if (request.File.Length > 10485760)
        {
            ModelState.AddModelError("file", "File size is more than 10MB, please upload a smaller size file.");
        }
    }
}