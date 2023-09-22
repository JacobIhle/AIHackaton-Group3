using Microsoft.AspNetCore.Mvc;
using SustainaBiteAPI.Repository;

namespace SustainaBiteAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RecipieController : ControllerBase
    {
        private readonly ChatRepository _chatRepository;

        public RecipieController(ChatRepository chatRepository) => _chatRepository = chatRepository;

        [HttpGet]
        public async Task<IActionResult> GetAsync([FromQuery] string[] ingredient)
        {
            return Ok(await _chatRepository.GetAsync(ingredient));
        }
    }
}
