using CodePrep.Application.Problem.DTOs;
using CodePrep.Application.Problem.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CodePrep.API.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class ProblemController : ControllerBase
{
    private readonly IProblemService _service;

    public ProblemController(IProblemService service)
    {
        _service = service;
    }

    // Public
    [AllowAnonymous]
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var result = await _service.GetAllAsync();
        return Ok(result);
    }

    // Public
    [AllowAnonymous]
    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var result = await _service.GetByIdAsync(id);

        if (result == null)
            return NotFound(new
            {
                Message = "Problem not found."
            });

        return Ok(result);
    }

    // Public
    [AllowAnonymous]
    [HttpGet("topic/{topicId:guid}")]
    public async Task<IActionResult> GetByTopic(Guid topicId)
    {
        var result = await _service.GetByTopicAsync(topicId);
        return Ok(result);
    }

    // Admin
    [HttpPost]
    public async Task<IActionResult> Create(CreateProblemDto dto)
    {
        var id = await _service.CreateAsync(dto);

        return Ok(new
        {
            Message = "Problem created successfully.",
            Id = id
        });
    }

    // Admin
    [HttpPut("{id:guid}")]
    public async Task<IActionResult> Update(Guid id, UpdateProblemDto dto)
    {
        dto.Id = id;

        var updated = await _service.UpdateAsync(dto);

        if (!updated)
            return NotFound(new
            {
                Message = "Problem not found."
            });

        return Ok(new
        {
            Message = "Problem updated successfully."
        });
    }

    // Admin
    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        var deleted = await _service.DeleteAsync(id);

        if (!deleted)
            return NotFound(new
            {
                Message = "Problem not found."
            });

        return Ok(new
        {
            Message = "Problem deleted successfully."
        });
    }
}