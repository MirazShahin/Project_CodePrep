using CodePrep.Application.Interview.DTOs;
using CodePrep.Application.Interview.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CodePrep.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class InterviewController : ControllerBase
{
    private readonly IInterviewService _interviewService;

    public InterviewController(IInterviewService interviewService)
    {
        _interviewService = interviewService;
    }

    // GET: api/Interview
    [HttpGet]
    public async Task<ActionResult<IEnumerable<InterviewQuestionDto>>> GetAll()
    {
        var questions = await _interviewService.GetAllAsync();
        return Ok(questions);
    }

    // GET: api/Interview/{id}
    [HttpGet("{id:guid}")]
    public async Task<ActionResult<InterviewQuestionDto>> GetById(Guid id)
    {
        var question = await _interviewService.GetByIdAsync(id);

        if (question == null)
        {
            return NotFound(new { message = $"Interview question with ID '{id}' was not found." });
        }

        return Ok(question);
    }

    // GET: api/Interview/topic/{topicId}
    [HttpGet("topic/{topicId:guid}")]
    public async Task<ActionResult<IEnumerable<InterviewQuestionDto>>> GetByTopic(Guid topicId)
    {
        var questions = await _interviewService.GetByTopicAsync(topicId);
        return Ok(questions);
    }

    // POST: api/Interview
    [HttpPost]
    public async Task<ActionResult<Guid>> Create([FromBody] CreateInterviewQuestionDto dto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var id = await _interviewService.CreateAsync(dto);

        return CreatedAtAction(nameof(GetById), new { id }, id);
    }

    // PUT: api/Interview/{id}
    [HttpPut("{id:guid}")]
    public async Task<IActionResult> Update(Guid id, [FromBody] UpdateInterviewQuestionDto dto)
    {
        if (id != dto.Id)
        {
            return BadRequest(new { message = "ID in route does not match ID in body." });
        }

        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var success = await _interviewService.UpdateAsync(dto);

        if (!success)
        {
            return NotFound(new { message = $"Interview question with ID '{id}' was not found." });
        }

        return NoContent();
    }

    // DELETE: api/Interview/{id}
    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        var success = await _interviewService.DeleteAsync(id);

        if (!success)
        {
            return NotFound(new { message = $"Interview question with ID '{id}' was not found." });
        }

        return NoContent();
    }
}