using CodePrep.Application.DTOs.TestCase;

namespace CodePrep.Application.Interfaces;

public interface ITestCaseService
{
    Task<TestCaseResponseDto> CreateAsync(CreateTestCaseRequestDto request);

    Task<List<TestCaseResponseDto>> GetByQuestionIdAsync(Guid questionId);

    Task UpdateAsync(Guid id, UpdateTestCaseRequestDto request);

    Task DeleteAsync(Guid id);
}