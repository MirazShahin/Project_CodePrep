using CodePrep.Application.Interfaces;
using CodePrep.Domain.Entities;
using CodePrep.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace CodePrep.Infrastructure.Repositories;

public class InterviewRepository : IInterviewRepository
{
    private readonly ApplicationDbContext _context;

    public InterviewRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<InterviewQuestion>> GetAllAsync()
    {
        return await _context.InterviewQuestions
            .Include(x => x.Topic)
            .ToListAsync();
    }

    public async Task<InterviewQuestion?> GetByIdAsync(Guid id)
    {
        return await _context.InterviewQuestions
            .Include(x => x.Topic)
            .FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task<IEnumerable<InterviewQuestion>> GetByTopicAsync(Guid topicId)
    {
        return await _context.InterviewQuestions
            .Where(x => x.TopicId == topicId)
            .Include(x => x.Topic)
            .ToListAsync();
    }

    public async Task AddAsync(InterviewQuestion entity)
    {
        await _context.InterviewQuestions.AddAsync(entity);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(InterviewQuestion entity)
    {
        _context.InterviewQuestions.Update(entity);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(InterviewQuestion entity)
    {
        _context.InterviewQuestions.Remove(entity);
        await _context.SaveChangesAsync();
    }
    public async Task<int> CountAsync()
    {
        return await _context.InterviewQuestions.CountAsync();
    }
}