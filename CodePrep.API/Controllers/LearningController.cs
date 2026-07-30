using CodePrep.Application.Learning.DTOs;
using CodePrep.Application.Learning.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CodePrep.API.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class LearningController : ControllerBase
{
    private readonly ILearningService _service;

    public LearningController(ILearningService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var result = await _service.GetAllAsync();
        return Ok(result);
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var result = await _service.GetByIdAsync(id);

        if (result == null)
            return NotFound();

        return Ok(result);
    }

    [HttpGet("topic/{topicId:guid}")]
    public async Task<IActionResult> GetByTopic(Guid topicId)
    {
        var result = await _service.GetByTopicAsync(topicId);
        return Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateLearningContentDto dto)
    {
        var id = await _service.CreateAsync(dto);

        return Ok(new
        {
            Message = "Learning content created successfully.",
            Id = id
        });
    }

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> Update(
        Guid id,
        [FromBody] UpdateLearningContentDto dto)
    {
        dto.Id = id;

        var updated = await _service.UpdateAsync(dto);

        if (!updated)
            return NotFound(new
            {
                Message = "Learning content not found."
            });

        return Ok(new
        {
            Message = "Learning content updated successfully."
        });
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        var deleted = await _service.DeleteAsync(id);

        if (!deleted)
            return NotFound(new
            {
                Message = "Learning content not found."
            });

        return Ok(new
        {
            Message = "Learning content deleted successfully."
        });
    }
}