using System.Security.Claims;
using CodePrep.Application.DTOs.Submission;
using CodePrep.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CodePrep.API.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class SubmissionController : ControllerBase
{
    private readonly ISubmissionService _submissionService;

    public SubmissionController(ISubmissionService submissionService)
    {
        _submissionService = submissionService;
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreateSubmissionRequestDto request)
    {
        var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

        if (userIdClaim == null)
            return Unauthorized();

        var userId = Guid.Parse(userIdClaim);

        var result = await _submissionService.CreateAsync(userId, request);

        return Ok(result);
    }

    [HttpGet("my")]
    public async Task<IActionResult> MySubmissions()
    {
        var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

        if (userIdClaim == null)
            return Unauthorized();

        var userId = Guid.Parse(userIdClaim);

        var result = await _submissionService.GetMySubmissionsAsync(userId);

        return Ok(result);
    }

    [Authorize(Roles = "Admin")]
    [HttpGet("question/{questionId:guid}")]
    public async Task<IActionResult> QuestionSubmissions(Guid questionId)
    {
        var result = await _submissionService.GetQuestionSubmissionsAsync(questionId);

        return Ok(result);
    }
}