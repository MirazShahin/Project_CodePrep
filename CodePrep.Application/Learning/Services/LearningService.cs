using CodePrep.Application.Interfaces;
using CodePrep.Application.Learning.DTOs;
using CodePrep.Application.Learning.Interfaces;
using CodePrep.Domain.Common.Enums;
using CodePrep.Domain.Entities;

namespace CodePrep.Application.Learning.Services;

public class LearningService : ILearningService
{
    private readonly ILearningRepository _repository;

    public LearningService(ILearningRepository repository)
    {
        _repository = repository;
    }

    public async Task<IEnumerable<LearningContentDto>> GetAllAsync()
    {
        var data = await _repository.GetAllAsync();

        return data.Select(x => new LearningContentDto
        {
            Id = x.Id,
            Title = x.Title,
            Description = x.Description,
            Content = x.Content,
            TopicId = x.TopicId,
            TopicName = x.Topic.Name,
            Difficulty = x.Difficulty.ToString(),
            EstimatedMinutes = x.EstimatedMinutes
        });
    }

    // int id এর জায়গায় Guid id
    public async Task<LearningContentDto?> GetByIdAsync(Guid id)
    {
        var x = await _repository.GetByIdAsync(id);

        if (x == null)
            return null;

        return new LearningContentDto
        {
            Id = x.Id,
            Title = x.Title,
            Description = x.Description,
            Content = x.Content,
            TopicId = x.TopicId,
            TopicName = x.Topic.Name,
            Difficulty = x.Difficulty.ToString(),
            EstimatedMinutes = x.EstimatedMinutes
        };
    }

    // int topicId এর জায়গায় Guid topicId
    public async Task<IEnumerable<LearningContentDto>> GetByTopicAsync(Guid topicId)
    {
        var data = await _repository.GetByTopicAsync(topicId);

        return data.Select(x => new LearningContentDto
        {
            Id = x.Id,
            Title = x.Title,
            Description = x.Description,
            Content = x.Content,
            TopicId = x.TopicId,
            TopicName = x.Topic.Name,
            Difficulty = x.Difficulty.ToString(),
            EstimatedMinutes = x.EstimatedMinutes
        });
    }

    // Task<int> এর জায়গায় Task<Guid>
    public async Task<Guid> CreateAsync(CreateLearningContentDto dto)
    {
        Enum.TryParse<Difficulty>(dto.Difficulty, true, out var difficulty);

        var entity = new LearningContent
        {
            Id = Guid.NewGuid(), // নতুন Guid তৈরি হচ্ছে
            Title = dto.Title,
            Description = dto.Description,
            Content = dto.Content,
            TopicId = dto.TopicId,
            Difficulty = difficulty,
            EstimatedMinutes = dto.EstimatedMinutes
        };

        await _repository.AddAsync(entity);

        return entity.Id; // Guid রিটার্ন করবে
    }

    public async Task<bool> UpdateAsync(UpdateLearningContentDto dto)
    {
        var entity = await _repository.GetByIdAsync(dto.Id);

        if (entity == null)
            return false;

        Enum.TryParse<Difficulty>(dto.Difficulty, true, out var difficulty);

        entity.Title = dto.Title;
        entity.Description = dto.Description;
        entity.Content = dto.Content;
        entity.TopicId = dto.TopicId;
        entity.Difficulty = difficulty;
        entity.EstimatedMinutes = dto.EstimatedMinutes;
        entity.UpdatedAt = DateTime.UtcNow;

        await _repository.UpdateAsync(entity);

        return true;
    }

    // int id এর জায়গায় Guid id
    public async Task<bool> DeleteAsync(Guid id)
    {
        var entity = await _repository.GetByIdAsync(id);

        if (entity == null)
            return false;

        await _repository.DeleteAsync(entity);

        return true;
    }
}