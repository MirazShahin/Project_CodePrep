using CodePrep.Application.AI.DTOs;
using CodePrep.Application.AI.Interfaces;
using CodePrep.Application.AI.Prompts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CodePrep.API.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class CPAssistantController : ControllerBase
{
    private readonly IAIProvider _aiProvider;

    public CPAssistantController(IAIProvider aiProvider)
    {
        _aiProvider = aiProvider;
    }

    [HttpPost("hint-mode")]
    public async Task<IActionResult> GetHints([FromBody] CPAssistantRequestDto request)
    {
        var systemInstruction = PromptBuilder.BuildHintModeSystemInstruction();
        var userPrompt = PromptBuilder.BuildCPUserPrompt(request.ProblemStatement, request.UserCode, request.Language);

        var result = await _aiProvider.GenerateWithSystemInstructionAsync(systemInstruction, userPrompt);
        return Ok(result);
    }

    [HttpPost("edge-cases")]
    public async Task<IActionResult> FindEdgeCases([FromBody] CPAssistantRequestDto request)
    {
        var systemInstruction = PromptBuilder.BuildEdgeCaseFinderSystemInstruction();
        var userPrompt = PromptBuilder.BuildCPUserPrompt(request.ProblemStatement, request.UserCode, request.Language);

        var result = await _aiProvider.GenerateWithSystemInstructionAsync(systemInstruction, userPrompt);
        return Ok(result);
    }

    [HttpPost("generate-testcases")]
    public async Task<IActionResult> GenerateTestCases([FromBody] CPAssistantRequestDto request)
    {
        var systemInstruction = PromptBuilder.BuildTestCaseGeneratorSystemInstruction();
        var userPrompt = $"### Problem Statement:\n{request.ProblemStatement}";

        var result = await _aiProvider.GenerateWithSystemInstructionAsync(systemInstruction, userPrompt);
        return Ok(result);
    }

    [HttpPost("editorial")]
    public async Task<IActionResult> GenerateEditorial([FromBody] CPAssistantRequestDto request)
    {
        var systemInstruction = PromptBuilder.BuildEditorialGeneratorSystemInstruction();
        var userPrompt = $"### Problem Statement:\n{request.ProblemStatement}";

        var result = await _aiProvider.GenerateWithSystemInstructionAsync(systemInstruction, userPrompt);
        return Ok(result);
    }

    [HttpPost("explain-code")]
    public async Task<IActionResult> ExplainCode(
    [FromBody] ExplainCodeRequestDto request)
    {
        var systemInstruction =
            PromptBuilder.BuildExplainSystemInstruction();

        var userPrompt =
            PromptBuilder.BuildUserPrompt(
                request.Language,
                request.Code);

        var result =
            await _aiProvider.GenerateWithSystemInstructionAsync(
                systemInstruction,
                userPrompt);

        return Ok(result);
    }


}