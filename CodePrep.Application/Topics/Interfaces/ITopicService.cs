using CodePrep.Application.DTOs.Topic;
using CodePrep.Application.Topics.DTOs;

namespace CodePrep.Application.Topics.Interfaces;

public interface ITopicService
{
    Task<IEnumerable<GroupedTopicDto>> GetGroupedTopicsAsync();

    Task<List<TopicDto>> GetAllAsync();

    Task<TopicDto> GetByIdAsync(Guid id);

    Task<TopicDto> CreateAsync(CreateTopicRequestDto request);

    Task UpdateAsync(Guid id, UpdateTopicRequestDto request);

    Task DeleteAsync(Guid id);

    // User Progress Methods
    Task ToggleTopicProgressAsync(string userId, ToggleProgressRequestDto request);

    Task<List<TopicProgressResponseDto>> GetUserProgressAsync(string userId);

    // Search & Filter
    Task<TopicDetailsDto?> GetTopicDetailsAsync(Guid id);
    Task<List<TopicDto>> SearchTopicsAsync(string? searchTerm, string? categoryName);
}