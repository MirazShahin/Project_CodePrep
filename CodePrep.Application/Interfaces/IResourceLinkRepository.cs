using CodePrep.Domain.Entities;

namespace CodePrep.Application.Interfaces;

public interface IResourceLinkRepository
{
    Task<List<ResourceLink>> GetByTopicIdAsync(Guid topicId);

    Task<ResourceLink?> GetByIdAsync(Guid id);

    Task AddAsync(ResourceLink resource);

    Task UpdateAsync(ResourceLink resource);

    Task DeleteAsync(ResourceLink resource);
}