using Microsoft.AspNetCore.Http;
using Obelyx.Core.Entities;

namespace Obelyx.Data.Services
{
    public interface IImageService
    {
        Task<string?> UpdateImage(Game game, IFormFile image);
        bool DeleteImage(string? imagePath);
    }
}
