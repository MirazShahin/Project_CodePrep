using CodePrep.Application.Common.Exceptions;
using CodePrep.Application.Interfaces;
using CodePrep.Application.Topics.DTOs;
using CodePrep.Application.Topics.Interfaces;
using CodePrep.Domain.Entities;
using CodePrep.Application.DTOs.Topic;

namespace CodePrep.Application.Services;

public class TopicService : ITopicService
{
    private readonly ITopicRepository _topicRepository;

    public TopicService(ITopicRepository topicRepository)
    {
        _topicRepository = topicRepository;
    }

    public async Task<IEnumerable<GroupedTopicDto>> GetGroupedTopicsAsync()
    {
        var topics = await _topicRepository.GetAllWithCategoryAsync();

        return topics
            .GroupBy(t => string.IsNullOrWhiteSpace(t.CategoryName)
                ? "General Topics"
                : t.CategoryName)
            .Select(g => new GroupedTopicDto
            {
                CategoryName = g.Key,
                Topics = g.Select(x => new TopicDto
                {
                    Id = x.Id,
                    Name = x.Name,
                    Description = x.Description,
                    CategoryName = x.CategoryName,
                    IsActive = x.IsActive,

                    Resources = x.Resources.Select(r => new ResourceLinkDto
                    {
                        Title = r.Title,
                        Website = r.Website,
                        Url = r.Url
                    }).ToList()

                }).ToList()
            });
    }

    public async Task<TopicDto> CreateAsync(CreateTopicRequestDto request)
    {
        var existing = await _topicRepository.GetByNameAsync(request.Name);

        if (existing != null)
            throw new DuplicateException("Topic already exists.");

        var topic = new Topic
        {
            Id = Guid.NewGuid(),
            Name = request.Name,
            Description = request.Description,
            CategoryName = request.CategoryName,
            IsActive = true,

            Resources = request.Resources?.Select(r => new ResourceLink
            {
                Title = r.Title,
                Website = r.Website,
                Url = r.Url
            }).ToList() ?? new List<ResourceLink>()
        };

        await _topicRepository.AddAsync(topic);

        return await GetByIdAsync(topic.Id);
    }

    public async Task<List<TopicDto>> GetAllAsync()
    {
        var topics = await _topicRepository.GetAllAsync();

        return topics.Select(x => new TopicDto
        {
            Id = x.Id,
            Name = x.Name,
            Description = x.Description,
            CategoryName = x.CategoryName,
            IsActive = x.IsActive
        }).ToList();
    }

    public async Task<TopicDto> GetByIdAsync(Guid id)
    {
        var topic = await _topicRepository.GetByIdAsync(id);

        if (topic == null)
            throw new NotFoundException("Topic not found.");

        return new TopicDto
        {
            Id = topic.Id,
            Name = topic.Name,
            Description = topic.Description,
            CategoryName = topic.CategoryName,
            IsActive = topic.IsActive,

            Resources = topic.Resources.Select(r => new ResourceLinkDto
            {
                Title = r.Title,
                Website = r.Website,
                Url = r.Url
            }).ToList()
        };
    }

    public async Task UpdateAsync(Guid id, UpdateTopicRequestDto request)
    {
        var topic = await _topicRepository.GetByIdAsync(id);

        if (topic == null)
            throw new NotFoundException("Topic not found.");

        topic.Name = request.Name;
        topic.Description = request.Description;
        topic.CategoryName = request.CategoryName;

        await _topicRepository.UpdateAsync(topic);
    }

    public async Task DeleteAsync(Guid id)
    {
        var topic = await _topicRepository.GetByIdAsync(id);

        if (topic == null)
            throw new NotFoundException("Topic not found.");

        await _topicRepository.DeleteAsync(topic);
    }

    public async Task ToggleTopicProgressAsync(string userId, ToggleProgressRequestDto request)
    {
        if (!Guid.TryParse(userId, out var parsedUserId))
            throw new ArgumentException("Invalid User ID.");

        var progress = await _topicRepository.GetUserProgressAsync(parsedUserId, request.TopicId);

        if (progress == null)
        {
            progress = new UserTopicProgress
            {
                Id = Guid.NewGuid(),
                UserId = userId,
                TopicId = request.TopicId,
                IsCompleted = true,
                CompletedAt = DateTime.UtcNow
            };

            await _topicRepository.AddUserProgressAsync(progress);
        }
        else
        {
            progress.IsCompleted = !progress.IsCompleted;
            progress.CompletedAt = progress.IsCompleted
                ? DateTime.UtcNow
                : null;
        }

        await _topicRepository.SaveChangesAsync();
    }

    public async Task<List<TopicProgressResponseDto>> GetUserProgressAsync(string userId)
    {
        // TODO: Implement later
        return new List<TopicProgressResponseDto>();
    }

    public async Task<List<TopicDto>> SearchTopicsAsync(string? searchTerm, string? categoryName)
    {
        var topics = await _topicRepository.GetAllAsync();

        if (!string.IsNullOrWhiteSpace(searchTerm))
        {
            topics = topics.Where(x =>
                x.Name.Contains(searchTerm, StringComparison.OrdinalIgnoreCase) ||
                x.Description.Contains(searchTerm, StringComparison.OrdinalIgnoreCase));
        }

        if (!string.IsNullOrWhiteSpace(categoryName))
        {
            topics = topics.Where(x =>
                x.CategoryName.Equals(categoryName, StringComparison.OrdinalIgnoreCase));
        }

        return topics.Select(x => new TopicDto
        {
            Id = x.Id,
            Name = x.Name,
            Description = x.Description,
            CategoryName = x.CategoryName,
            IsActive = x.IsActive
        }).ToList();
    }
    public async Task<TopicDetailsDto?> GetTopicDetailsAsync(Guid id)
    {
        var topic = await _topicRepository.GetTopicDetailsAsync(id);

        if (topic == null)
            return null;

        return new TopicDetailsDto
        {
            Id = topic.Id,
            Name = topic.Name,
            Description = topic.Description,
            CategoryName = topic.CategoryName,

            Resources = topic.Resources
                .OrderBy(x => x.DisplayOrder)
                .Select(x => new ResourceLinkDto
                {
                    Title = x.Title,
                    Website = x.Website,
                    Url = x.Url
                }).ToList()
        };
    }
}