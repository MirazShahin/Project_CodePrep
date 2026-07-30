using CodePrep.Application.Interfaces;
using CodePrep.Domain.Entities;
using CodePrep.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace CodePrep.Infrastructure.Repositories;

public class ProblemRepository : IProblemRepository
{
    private readonly ApplicationDbContext _context;

    public ProblemRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Problem>> GetAllAsync()
    {
        return await _context.Problems
            .Include(x => x.Topic)
            .ToListAsync();
    }

    public async Task<Problem?> GetByIdAsync(Guid id)
    {
        return await _context.Problems
            .Include(x => x.Topic)
            .FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task<IEnumerable<Problem>> GetByTopicAsync(Guid topicId)
    {
        return await _context.Problems
            .Where(x => x.TopicId == topicId)
            .Include(x => x.Topic)
            .ToListAsync();
    }

    public async Task AddAsync(Problem problem)
    {
        await _context.Problems.AddAsync(problem);
    }

    public Task UpdateAsync(Problem problem)
    {
        _context.Problems.Update(problem);
        return Task.CompletedTask;
    }

    public Task DeleteAsync(Problem problem)
    {
        _context.Problems.Remove(problem);
        return Task.CompletedTask;
    }

    public async Task SaveChangesAsync()
    {
        await _context.SaveChangesAsync();
    }
    public async Task<int> CountAsync()
    {
        return await _context.Problems.CountAsync();
    }
}