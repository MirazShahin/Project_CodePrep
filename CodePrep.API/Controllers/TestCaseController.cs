using CodePrep.Application.DTOs.TestCase;
using CodePrep.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CodePrep.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TestCaseController : ControllerBase
{
    private readonly ITestCaseService _testCaseService;

    public TestCaseController(ITestCaseService testCaseService)
    {
        _testCaseService = testCaseService;
    }

    // Create Test Case
    [Authorize(Roles = "Admin")]
    [HttpPost]
    public async Task<IActionResult> Create(CreateTestCaseRequestDto request)
    {
        var result = await _testCaseService.CreateAsync(request);
        return Ok(result);
    }

    // Get Test Cases by Question
    [HttpGet("question/{questionId:guid}")]
    public async Task<IActionResult> GetByQuestion(Guid questionId)
    {
        var result = await _testCaseService.GetByQuestionIdAsync(questionId);
        return Ok(result);
    }

    // Update Test Case
    [Authorize(Roles = "Admin")]
    [HttpPut("{id:guid}")]
    public async Task<IActionResult> Update(Guid id, UpdateTestCaseRequestDto request)
    {
        await _testCaseService.UpdateAsync(id, request);

        return Ok(new
        {
            success = true,
            message = "Test case updated successfully."
        });
    }

    // Delete Test Case
    [Authorize(Roles = "Admin")]
    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        await _testCaseService.DeleteAsync(id);

        return Ok(new
        {
            success = true,
            message = "Test case deleted successfully."
        });
    }
}