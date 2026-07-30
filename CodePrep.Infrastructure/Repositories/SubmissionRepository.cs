using CodePrep.Application.Interfaces;
using CodePrep.Domain.Entities;
using CodePrep.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace CodePrep.Infrastructure.Repositories;

public class SubmissionRepository : ISubmissionRepository
{
    private readonly ApplicationDbContext _context;

    public SubmissionRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task AddAsync(Submission submission)
    {
        await _context.Submissions.AddAsync(submission);
    }

    public async Task<Submission?> GetByIdAsync(Guid id)
    {
        return await _context.Submissions.FindAsync(id);
    }

    public async Task<List<Submission>> GetByUserIdAsync(Guid userId)
    {
        return await _context.Submissions
            .Where(x => x.UserId == userId)
            .OrderByDescending(x => x.SubmittedAt)
            .ToListAsync();
    }

    public async Task<List<Submission>> GetByQuestionIdAsync(Guid questionId)
    {
        return await _context.Submissions
            .Where(x => x.QuestionId == questionId)
            .OrderByDescending(x => x.SubmittedAt)
            .ToListAsync();
    }

    public Task UpdateAsync(Submission submission)
    {
        _context.Submissions.Update(submission);
        return Task.CompletedTask;
    }

    public async Task SaveChangesAsync()
    {
        await _context.SaveChangesAsync();
    }
}