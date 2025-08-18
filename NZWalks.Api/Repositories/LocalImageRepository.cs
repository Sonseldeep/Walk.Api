using NZWalks.Api.Data;
using NZWalks.Api.Models.Domain;

namespace NZWalks.Api.Repositories;

public class LocalImageRepository : IImageRepository
{
    private readonly IWebHostEnvironment _webHostEnvironment;
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly ApplicationDbContext _dbContext;

    public LocalImageRepository(IWebHostEnvironment webHostEnvironment, IHttpContextAccessor httpContextAccessor,
        ApplicationDbContext dbContext)
    {
        _webHostEnvironment = webHostEnvironment;
        _httpContextAccessor = httpContextAccessor;
        _dbContext = dbContext;
    }

    public async Task<Image> Upload(Image image)
    {
        // Ensure Images folder exists
        var imagesFolder = Path.Combine(_webHostEnvironment.ContentRootPath, "Images");
        if (!Directory.Exists(imagesFolder))
            Directory.CreateDirectory(imagesFolder);

        // Get file extension
        image.FileExtension = Path.GetExtension(image.File.FileName);

        // Unique filename
        var uniqueFileName =
            $"{Guid.NewGuid()}_{Path.GetFileNameWithoutExtension(image.File.FileName)}{image.FileExtension}";
        image.FileName = uniqueFileName;

        // Local path
        var localFilePath = Path.Combine(imagesFolder, uniqueFileName);

        // Save file locally
        await using var stream = new FileStream(localFilePath, FileMode.Create);
        await image.File.CopyToAsync(stream);

        // Public URL
        image.FilePath =
            $"{_httpContextAccessor.HttpContext?.Request.Scheme}://{_httpContextAccessor.HttpContext?.Request.Host}/Images/{uniqueFileName}";

        // Save to DB
        await _dbContext.Images.AddAsync(image);
        await _dbContext.SaveChangesAsync();

        return image;
    }
}

//     public async Task<Image> Upload(Image image)
//     {
//         var localFilePath = Path.Combine(_webHostEnvironment.ContentRootPath,"Images",
//             
//             $"{image.FileName}{image.FileExtension}")
//             ;
//         // upload image to local path
//         await using var stream = new FileStream(localFilePath, FileMode.Create);
//         await image.File.CopyToAsync(stream);
//
//         // https://localhost:5000/Images/filename.extension
//         var urlFilePath =
//             $"{_httpContextAccessor.HttpContext?.Request.Scheme}://{_httpContextAccessor.HttpContext?.Request.Host}{_httpContextAccessor.HttpContext?.Request.PathBase}/Images/{image.FileName}{image.FileExtension}";
//         image.FilePath = urlFilePath;
//         // Add image to Image table
//         await _dbContext.Images.AddAsync(image);
//         await _dbContext.SaveChangesAsync();
//
//         return image;
//
//
//     }
// }