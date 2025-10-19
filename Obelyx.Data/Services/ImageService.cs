using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Obelyx.Core.Entities;

namespace Obelyx.Data.Services
{
    public class ImageService : IImageService
    {
        private readonly IWebHostEnvironment _env;

        public ImageService (IWebHostEnvironment env)
        {
            _env = env;
        }

        // Save or replace image on a game
        public async Task<string?> UpdateImage(Game game, IFormFile image)
        {
            if (image != null && image.Length > 0)
            {
                // Delete image if game already has one
                if (DeleteImage(game.ImagePath))
                {
                    game.ImagePath = null;
                }

                var uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images", "covers");

                if (!Directory.Exists(uploadsFolder))
                {
                    Directory.CreateDirectory(uploadsFolder);
                }

                // Image name inherits game guid
                var uniqueFileName = $"{game.Id}{Path.GetExtension(image.FileName)}";
                var filePath = Path.Combine(uploadsFolder, uniqueFileName);
            
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    await image.CopyToAsync(fileStream);
                }

                var finalPath = $"/images/covers/{uniqueFileName}";
                return finalPath;
            }

            return null;
        }

        // Remove image file from storage if present
        public bool DeleteImage(string? imagePath)
        {
            if (!string.IsNullOrWhiteSpace(imagePath))
            {
                // Replace image slashes in image path to OS-specific separators
                var relativePath = imagePath.TrimStart('/').Replace('/', Path.DirectorySeparatorChar);

                var fullImagePath = Path.Combine(_env.WebRootPath, relativePath);

                try
                {
                    if (File.Exists(fullImagePath))
                    {
                        File.Delete(fullImagePath);
                        return true;
                    }
                }
                catch (IOException ex)
                {
                    // TODO: log exception
                }
                catch (UnauthorizedAccessException ex)
                {
                    // TODO: log exception
                }
            }

            return false;
        }
    }
}
