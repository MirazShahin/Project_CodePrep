using CodePrep.Application.Common.Exceptions;
using CodePrep.Application.DTOs.TestCase;
using CodePrep.Application.Interfaces;
using CodePrep.Domain.Entities;

namespace CodePrep.Application.Services;

public class TestCaseService : ITestCaseService
{
    private readonly ITestCaseRepository _testCaseRepository;
    private readonly IQuestionRepository _questionRepository;

    public TestCaseService(
        ITestCaseRepository testCaseRepository,
        IQuestionRepository questionRepository)
    {
        _testCaseRepository = testCaseRepository;
        _questionRepository = questionRepository;
    }

    public async Task<TestCaseResponseDto> CreateAsync(CreateTestCaseRequestDto request)
    {
        var question = await _questionRepository.GetByIdAsync(request.QuestionId);

        if (question == null)
            throw new NotFoundException("Question not found.");

        var testCase = new TestCase
        {
            QuestionId = request.QuestionId,
            Input = request.Input,
            ExpectedOutput = request.ExpectedOutput,
            IsHidden = request.IsHidden,
            Order = request.Order
        };

        await _testCaseRepository.AddAsync(testCase);
        await _testCaseRepository.SaveChangesAsync();

        return new TestCaseResponseDto
        {
            Id = testCase.Id,
            QuestionId = testCase.QuestionId,
            Input = testCase.Input,
            ExpectedOutput = testCase.ExpectedOutput,
            IsHidden = testCase.IsHidden,
            Order = testCase.Order
        };
    }

    public async Task<List<TestCaseResponseDto>> GetByQuestionIdAsync(Guid questionId)
    {
        var testCases = await _testCaseRepository.GetByQuestionIdAsync(questionId);

        return testCases.Select(tc => new TestCaseResponseDto
        {
            Id = tc.Id,
            QuestionId = tc.QuestionId,
            Input = tc.Input,
            ExpectedOutput = tc.ExpectedOutput,
            IsHidden = tc.IsHidden,
            Order = tc.Order
        }).ToList();
    }

    public async Task UpdateAsync(Guid id, UpdateTestCaseRequestDto request)
    {
        var testCase = await _testCaseRepository.GetByIdAsync(id);

        if (testCase == null)
            throw new NotFoundException("Test case not found.");

        testCase.Input = request.Input;
        testCase.ExpectedOutput = request.ExpectedOutput;
        testCase.IsHidden = request.IsHidden;
        testCase.Order = request.Order;

        await _testCaseRepository.UpdateAsync(testCase);
        await _testCaseRepository.SaveChangesAsync();
    }

    public async Task DeleteAsync(Guid id)
    {
        var testCase = await _testCaseRepository.GetByIdAsync(id);

        if (testCase == null)
            throw new NotFoundException("Test case not found.");

        await _testCaseRepository.DeleteAsync(testCase);
        await _testCaseRepository.SaveChangesAsync();
    }
}