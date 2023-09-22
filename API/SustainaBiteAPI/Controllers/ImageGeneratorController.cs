using Microsoft.AspNetCore.Mvc;
using SustainaBiteAPI.Repository;

namespace SustainaBiteAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImageGeneratorController : ControllerBase
    {
        private readonly ImageGeneratorRepository _imageGeneratorRepository;

        public ImageGeneratorController(ImageGeneratorRepository imageGeneratorRepository) => _imageGeneratorRepository = imageGeneratorRepository;

        [HttpGet]
        public async Task<IActionResult> GetImageAsync([FromQuery] string query)
        {
            var image = await _imageGeneratorRepository.GenerateImageAsync(query);
            return File(image, "image/jpeg");
        }
    }
}
