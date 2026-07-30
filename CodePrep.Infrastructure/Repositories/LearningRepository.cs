using CodePrep.Application.Interfaces;
using CodePrep.Domain.Entities;
using CodePrep.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace CodePrep.Infrastructure.Repositories;

public class LearningRepository : ILearningRepository
{
    private readonly ApplicationDbContext _context;

    public LearningRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<LearningContent>> GetAllAsync()
    {
        return await _context.LearningContents
            .Include(x => x.Topic)
            .ToListAsync();
    }

    public async Task<LearningContent?> GetByIdAsync(Guid id)
    {
        return await _context.LearningContents
            .Include(x => x.Topic)
            .FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task<IEnumerable<LearningContent>> GetByTopicAsync(Guid topicId)
    {
        return await _context.LearningContents
            .Where(x => x.TopicId == topicId)
            .Include(x => x.Topic)
            .ToListAsync();
    }

    public async Task AddAsync(LearningContent entity)
    {
        await _context.LearningContents.AddAsync(entity);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(LearningContent entity)
    {
        _context.LearningContents.Update(entity);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(LearningContent entity)
    {
        _context.LearningContents.Remove(entity);
        await _context.SaveChangesAsync();
    }
    public async Task<int> CountAsync()
    {
        return await _context.LearningContents.CountAsync();
    }
}