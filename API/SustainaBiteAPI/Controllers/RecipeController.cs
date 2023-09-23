using Microsoft.AspNetCore.Mvc;
using SustainaBiteAPI.Repository;

namespace SustainaBiteAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RecipeController : ControllerBase
    {
        private readonly ChatRepository _chatRepository;

        public RecipeController(ChatRepository chatRepository) => _chatRepository = chatRepository;

        [HttpGet]
        public async Task<IActionResult> GetAsync([FromQuery] string[] ingredient)
        {
            return Ok(await _chatRepository.GetAsync(ingredient));
        }

        [HttpGet("question")]
        public async Task<IActionResult> RequestAsync([FromQuery] string question)
        {
            return Ok(await _chatRepository.GetAsync(question));
        }

        [HttpGet("questionowndata")]
        public async Task<IActionResult> RequestOnOwnDataAsync([FromQuery] string question)
        {
            return Ok(await _chatRepository.Get2Async(question));
        }
    }
}
