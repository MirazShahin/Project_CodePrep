using CodePrep.Domain.Entities;

namespace CodePrep.Application.Interfaces;

public interface ITestCaseRepository
{
    Task AddAsync(TestCase testCase);

    Task<TestCase?> GetByIdAsync(Guid id);

    Task<List<TestCase>> GetByQuestionIdAsync(Guid questionId);

    Task UpdateAsync(TestCase testCase);

    Task DeleteAsync(TestCase testCase);

    Task SaveChangesAsync();
}