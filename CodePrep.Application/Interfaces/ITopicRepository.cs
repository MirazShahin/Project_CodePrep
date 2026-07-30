using CodePrep.Domain.Entities;

namespace CodePrep.Application.Interfaces;

public interface ITopicRepository
{
    Task<IEnumerable<Topic>> GetAllAsync();

    Task<Topic?> GetByIdAsync(Guid id);

    Task<IEnumerable<Topic>> GetAllWithCategoryAsync();

    Task<Topic?> GetByNameAsync(string name);

    Task AddAsync(Topic topic);

    Task UpdateAsync(Topic topic);

    Task DeleteAsync(Topic topic);

    Task<int> CountAsync();

    Task<UserTopicProgress?> GetUserProgressAsync(Guid userId, Guid topicId);

    Task AddUserProgressAsync(UserTopicProgress userProgress);

    Task SaveChangesAsync();

    Task<List<Topic>> GetByCategoryAsync(string category);

    Task<List<Topic>> SearchAsync(string keyword);

    Task<Topic?> GetTopicDetailsAsync(Guid id);
}
