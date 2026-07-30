using CodePrep.Application.Interfaces;
using CodePrep.Domain.Entities;
using CodePrep.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
namespace CodePrep.Infrastructure.Repositories;

public class TopicRepository : ITopicRepository
{
    private readonly ApplicationDbContext _context;

    public TopicRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Topic>> GetAllAsync()
    {
        return await _context.Topics
            .OrderBy(x => x.CategoryName)
            .ThenBy(x => x.Name)
            .ToListAsync();
    }

    public async Task<Topic?> GetByIdAsync(Guid id)
    {
        return await _context.Topics
            .Include(x => x.Resources)
            .FirstOrDefaultAsync(x => x.Id == id);
    }
    public async Task<IEnumerable<Topic>> GetAllWithCategoryAsync()
    {
        return await _context.Topics
            .OrderBy(x => x.CategoryName)
            .ThenBy(x => x.Name)
            .ToListAsync();
    }

    public async Task<Topic?> GetByNameAsync(string name)
    {
        return await _context.Topics
            .FirstOrDefaultAsync(x => x.Name == name);
    }

    public async Task AddAsync(Topic topic)
    {
        await _context.Topics.AddAsync(topic);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(Topic topic)
    {
        _context.Topics.Update(topic);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(Topic topic)
    {
        _context.Topics.Remove(topic);
        await _context.SaveChangesAsync();
    }

    public async Task<int> CountAsync()
    {
        return await _context.Topics.CountAsync();
    }

    

    public async Task<UserTopicProgress?> GetUserProgressAsync(Guid userId, Guid topicId)
    {
        // Converts Guid userId to string if your DB entity stores UserId as string
        return await _context.UserTopicProgresses
            .FirstOrDefaultAsync(x => x.UserId == userId.ToString() && x.TopicId == topicId);
    }

    public async Task AddUserProgressAsync(UserTopicProgress userProgress)
    {
        await _context.UserTopicProgresses.AddAsync(userProgress);
    }

    public async Task SaveChangesAsync()
    {
        await _context.SaveChangesAsync();

    }
    public async Task<List<Topic>> GetByCategoryAsync(string category)
    {
        return await _context.Topics
            .Where(x => x.CategoryName == category && x.IsActive)
            .OrderBy(x => x.Name)
            .ToListAsync();
    }
    public async Task<List<Topic>> SearchAsync(string keyword)
    {
        return await _context.Topics
            .Where(x =>
                x.Name.Contains(keyword) ||
                x.Description.Contains(keyword))
            .ToListAsync();
    }
    public async Task<Topic?> GetTopicDetailsAsync(Guid id)
    {
        return await _context.Topics
            .Include(x => x.Resources)
            .FirstOrDefaultAsync(x => x.Id == id);
    }
}