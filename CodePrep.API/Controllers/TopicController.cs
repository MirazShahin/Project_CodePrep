using CodePrep.Application.Topics.Interfaces;
using CodePrep.Application.Topics.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace CodePrep.API.Controllers;
[Authorize]
[ApiController]
[Route("api/[controller]")]
public class TopicController : ControllerBase
{
    private readonly ITopicService _topicService;

    public TopicController(ITopicService topicService)
    {
        _topicService = topicService;
    }

    // Get Grouped Topics (Competitive Programming & Interview Prep)
    [AllowAnonymous]
    [HttpGet("grouped")]
    public async Task<IActionResult> GetGrouped()
    {
        var result = await _topicService.GetGroupedTopicsAsync();
        return Ok(result);
    }

    // Get All Topics (Public)
    [AllowAnonymous]
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var result = await _topicService.GetAllAsync();
        return Ok(result);
    }

    // Get Topic By Id (Public)
    [AllowAnonymous]
    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var result = await _topicService.GetByIdAsync(id);
        return Ok(result);
    }

    // Search & Filter Topics
    // GET /api/topic/search?keyword=binary
    [AllowAnonymous]
    [HttpGet("search")]
    public async Task<IActionResult> Search(
    [FromQuery] string? keyword,
    [FromQuery] string? q,
    [FromQuery] string? category)
    {
        // "keyword" is the primary param name; "q" is kept for backward compatibility
        var searchTerm = keyword ?? q;
        var result = await _topicService.SearchTopicsAsync(searchTerm, category);
        return Ok(result);
    }

    // Create Topic (Admin)
    [Authorize]
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateTopicRequestDto request)
    {
        var result = await _topicService.CreateAsync(request);
        return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
    }

    // Update Topic (Admin)
    [Authorize]
    [HttpPut("{id:guid}")]
    public async Task<IActionResult> Update(Guid id, [FromBody] UpdateTopicRequestDto request)
    {
        await _topicService.UpdateAsync(id, request);

        return Ok(new
        {
            success = true,
            message = "Topic updated successfully."
        });
    }

    // Delete Topic (Admin)
    [Authorize]
    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        await _topicService.DeleteAsync(id);

        return Ok(new
        {
            success = true,
            message = "Topic deleted successfully."
        });
    }

    // User Progress (Mark as completed / Bookmark)
    [Authorize]
    [HttpPost("progress")]
    public async Task<IActionResult> ToggleProgress([FromBody] ToggleProgressRequestDto request)
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

        if (string.IsNullOrEmpty(userId))
        {
            return Unauthorized();
        }

        await _topicService.ToggleTopicProgressAsync(userId, request);

        return Ok(new { success = true, message = "Topic progress updated successfully." });
    }

    // Get Logged-in User's Topic Progress List
    [Authorize]
    [HttpGet("my-progress")]
    public async Task<IActionResult> GetMyProgress()
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

        if (string.IsNullOrEmpty(userId))
        {
            return Unauthorized();
        }

        var result = await _topicService.GetUserProgressAsync(userId);
        return Ok(result);
    }

    // GET /api/topic/category/{category}
    [AllowAnonymous]
    [HttpGet("category/{category}")]
    public async Task<IActionResult> GetByCategory(string category)
    {
        var topics = await _topicService.GetAllAsync();

        var result = topics
            .Where(x => x.CategoryName.Equals(category, StringComparison.OrdinalIgnoreCase))
            .OrderBy(x => x.Name)
            .ToList();

        return Ok(result);
    }

    [AllowAnonymous]
    [HttpGet("{id:guid}/details")]
    public async Task<IActionResult> Details(Guid id)
    {
        var result = await _topicService.GetTopicDetailsAsync(id);

        if (result == null)
            return NotFound();

        return Ok(result);
    }
}