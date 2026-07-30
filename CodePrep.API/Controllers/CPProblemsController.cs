using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using CodePrep.Application.Interfaces;

namespace CodePrep.API.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class CPProblemsController : ControllerBase
    {
        private readonly ICodeforcesService _cfService;

        public CPProblemsController(ICodeforcesService cfService)
        {
            _cfService = cfService;
        }

        [HttpGet("cf-problems")]
        public async Task<IActionResult> GetCFProblems(
            [FromQuery] string? tag,
            [FromQuery] int minRating = 800,
            [FromQuery] int maxRating = 1500)
        {
            var problems = await _cfService.GetProblemsAsync(tag, minRating, maxRating);
            return Ok(problems);
        }
    }
}