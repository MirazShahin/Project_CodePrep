using CodePrep.Application.Interfaces;
using CodePrep.Domain.Entities;
using CodePrep.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace CodePrep.Infrastructure.Repositories;

public class TestCaseRepository : ITestCaseRepository
{
    private readonly ApplicationDbContext _context;

    public TestCaseRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task AddAsync(TestCase testCase)
    {
        await _context.TestCases.AddAsync(testCase);
    }

    public async Task<TestCase?> GetByIdAsync(Guid id)
    {
        return await _context.TestCases.FindAsync(id);
    }

    public async Task<List<TestCase>> GetByQuestionIdAsync(Guid questionId)
    {
        return await _context.TestCases
            .Where(x => x.QuestionId == questionId)
            .OrderBy(x => x.Order)
            .ToListAsync();
    }

    public Task UpdateAsync(TestCase testCase)
    {
        _context.TestCases.Update(testCase);
        return Task.CompletedTask;
    }

    public Task DeleteAsync(TestCase testCase)
    {
        _context.TestCases.Remove(testCase);
        return Task.CompletedTask;
    }

    public async Task SaveChangesAsync()
    {
        await _context.SaveChangesAsync();
    }
}