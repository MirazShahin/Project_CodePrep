using CodePrep.Application.AI.DTOs;
using CodePrep.Application.AI.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace CodePrep.API.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class AIController : ControllerBase
{
    private readonly AIService _aiService;

    public AIController(AIService aiService)
    {
        _aiService = aiService;
    }

    [HttpPost("ask")] 
    public async Task<IActionResult> Ask(AIRequestDto request)
    {
        var result = await _aiService.AskAsync(request);
        return Ok(result);
    }
}