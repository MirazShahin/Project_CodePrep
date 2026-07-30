using CodePrep.Application.DTOs.Question;
using CodePrep.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CodePrep.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class QuestionController : ControllerBase
{
    private readonly IQuestionService _questionService;

    public QuestionController(IQuestionService questionService)
    {
        _questionService = questionService;
    }

    // POST: api/Question
    [HttpPost]
    public async Task<IActionResult> Create(CreateQuestionRequestDto request)
    {
        var result = await _questionService.CreateAsync(request);
        return Ok(result);
    }

    // GET: api/Question
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var result = await _questionService.GetAllAsync();
        return Ok(result);
    }

    // GET: api/Question/{id}
    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var result = await _questionService.GetByIdAsync(id);
        return Ok(result);
    }

    // PUT: api/Question/{id}
    [HttpPut("{id:guid}")]
    public async Task<IActionResult> Update(Guid id, UpdateQuestionRequestDto request)
    {
        await _questionService.UpdateAsync(id, request);

        return Ok(new
        {
            success = true,
            message = "Question updated successfully."
        });
    }

    // DELETE: api/Question/{id}
    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        await _questionService.DeleteAsync(id);

        return Ok(new
        {
            success = true,
            message = "Question deleted successfully."
        });
    }
}