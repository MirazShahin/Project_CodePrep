using CodePrep.Application.AI.DTOs;
using CodePrep.Application.AI.Interfaces;
using CodePrep.Application.AI.Prompts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CodePrep.API.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class TutorController : ControllerBase
{
    private readonly IAIProvider _aiProvider;

    public TutorController(IAIProvider aiProvider)
    {
        _aiProvider = aiProvider;
    }

    [HttpPost("generate-roadmap")]
    public async Task<IActionResult> GenerateRoadmap([FromBody] TutorRequestDto request)
    {
        var systemInstruction = PromptBuilder.BuildRoadmapSystemInstruction(request.SkillLevel, request.DurationInDays);
        var userPrompt = $"Please generate a detailed learning roadmap for the topic: '{request.TargetTopic}'.";

        var result = await _aiProvider.GenerateWithSystemInstructionAsync(systemInstruction, userPrompt);
        return Ok(result);
    }

    [HttpPost("generate-quiz")]
    public async Task<IActionResult> GenerateQuiz([FromBody] TutorRequestDto request)
    {
        var systemInstruction = PromptBuilder.BuildQuizSystemInstruction(request.SkillLevel, request.TotalQuestions);
        var userPrompt = $"Please generate an interactive technical quiz for the topic: '{request.TargetTopic}'.";

        var result = await _aiProvider.GenerateWithSystemInstructionAsync(systemInstruction, userPrompt);
        return Ok(result);
    }
}