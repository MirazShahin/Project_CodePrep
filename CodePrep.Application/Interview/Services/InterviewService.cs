using CodePrep.Application.Interfaces;
using CodePrep.Application.Interview.DTOs;
using CodePrep.Application.Interview.Interfaces;
using CodePrep.Domain.Entities;

namespace CodePrep.Application.Interview.Services;

public class InterviewService : IInterviewService
{
    private readonly IInterviewRepository _repository;

    public InterviewService(IInterviewRepository repository)
    {
        _repository = repository;
    }

    public async Task<IEnumerable<InterviewQuestionDto>> GetAllAsync()
    {
        var data = await _repository.GetAllAsync();

        return data.Select(x => new InterviewQuestionDto
        {
            Id = x.Id,
            Title = x.Title,
            Question = x.Question,
            Answer = x.Answer,
            Explanation = x.Explanation,
            Company = x.Company,
            Category = x.Category,
            Difficulty = x.Difficulty,
            TopicId = x.TopicId,
            TopicName = x.Topic.Name,
            IsPublished = x.IsPublished
        });
    }

    public async Task<InterviewQuestionDto?> GetByIdAsync(Guid id)
    {
        var entity = await _repository.GetByIdAsync(id);

        if (entity == null)
            return null;

        return new InterviewQuestionDto
        {
            Id = entity.Id,
            Title = entity.Title,
            Question = entity.Question,
            Answer = entity.Answer,
            Explanation = entity.Explanation,
            Company = entity.Company,
            Category = entity.Category,
            Difficulty = entity.Difficulty,
            TopicId = entity.TopicId,
            TopicName = entity.Topic.Name,
            IsPublished = entity.IsPublished
        };
    }

    public async Task<IEnumerable<InterviewQuestionDto>> GetByTopicAsync(Guid topicId)
    {
        var data = await _repository.GetByTopicAsync(topicId);

        return data.Select(x => new InterviewQuestionDto
        {
            Id = x.Id,
            Title = x.Title,
            Question = x.Question,
            Answer = x.Answer,
            Explanation = x.Explanation,
            Company = x.Company,
            Category = x.Category,
            Difficulty = x.Difficulty,
            TopicId = x.TopicId,
            TopicName = x.Topic.Name,
            IsPublished = x.IsPublished
        });
    }

    public async Task<Guid> CreateAsync(CreateInterviewQuestionDto dto)
    {
        var entity = new InterviewQuestion
        {
            Title = dto.Title,
            Question = dto.Question,
            Answer = dto.Answer,
            Explanation = dto.Explanation,
            Company = dto.Company,
            Category = dto.Category,
            Difficulty = dto.Difficulty,
            TopicId = dto.TopicId,
            IsPublished = true
        };

        await _repository.AddAsync(entity);

        return entity.Id;
    }

    public async Task<bool> UpdateAsync(UpdateInterviewQuestionDto dto)
    {
        var entity = await _repository.GetByIdAsync(dto.Id);

        if (entity == null)
            return false;

        entity.Title = dto.Title;
        entity.Question = dto.Question;
        entity.Answer = dto.Answer;
        entity.Explanation = dto.Explanation;
        entity.Company = dto.Company;
        entity.Category = dto.Category;
        entity.Difficulty = dto.Difficulty;
        entity.TopicId = dto.TopicId;
        entity.IsPublished = dto.IsPublished;
        entity.UpdatedAt = DateTime.UtcNow;

        await _repository.UpdateAsync(entity);

        return true;
    }

    public async Task<bool> DeleteAsync(Guid id)
    {
        var entity = await _repository.GetByIdAsync(id);

        if (entity == null)
            return false;

        await _repository.DeleteAsync(entity);

        return true;
    }
}