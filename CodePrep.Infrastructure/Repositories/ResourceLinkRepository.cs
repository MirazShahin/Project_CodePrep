using CodePrep.Application.Interfaces;
using CodePrep.Domain.Entities;
using CodePrep.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace CodePrep.Infrastructure.Repositories;

public class ResourceLinkRepository : IResourceLinkRepository
{
    private readonly ApplicationDbContext _context;

    public ResourceLinkRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<List<ResourceLink>> GetByTopicIdAsync(Guid topicId)
    {
        return await _context.ResourceLinks
            .Where(x => x.TopicId == topicId)
            .OrderBy(x => x.DisplayOrder)
            .ToListAsync();
    }

    public async Task<ResourceLink?> GetByIdAsync(Guid id)
    {
        return await _context.ResourceLinks.FindAsync(id);
    }

    public async Task AddAsync(ResourceLink resource)
    {
        _context.ResourceLinks.Add(resource);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(ResourceLink resource)
    {
        _context.ResourceLinks.Update(resource);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(ResourceLink resource)
    {
        _context.ResourceLinks.Remove(resource);
        await _context.SaveChangesAsync();
    }
}