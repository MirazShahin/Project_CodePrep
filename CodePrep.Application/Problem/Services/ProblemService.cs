using CodePrep.Application.Interfaces;
using CodePrep.Application.Problem.DTOs;
using CodePrep.Application.Problem.Interfaces;
using ProblemEntity = CodePrep.Domain.Entities.Problem;

namespace CodePrep.Application.Problem.Services;

public class ProblemService : IProblemService
{
    private readonly IProblemRepository _repository;

    public ProblemService(IProblemRepository repository)
    {
        _repository = repository;
    }

    public async Task<IEnumerable<ProblemDto>> GetAllAsync()
    {
        var data = await _repository.GetAllAsync();

        return data.Select(x => new ProblemDto
        {
            Id = x.Id,
            Title = x.Title,
            Slug = x.Slug,
            TopicId = x.TopicId,
            TopicName = x.Topic.Name,
            Judge = x.Judge,
            ProblemType = x.ProblemType,
            Difficulty = x.Difficulty,
            Rating = x.Rating,
            ProblemUrl = x.ProblemUrl,
            IsPublished = x.IsPublished,
            Tags = x.Tags
        });
    }

    public async Task<ProblemDto?> GetByIdAsync(Guid id)
    {
        var x = await _repository.GetByIdAsync(id);

        if (x == null)
            return null;

        return new ProblemDto
        {
            Id = x.Id,
            Title = x.Title,
            Slug = x.Slug,
            TopicId = x.TopicId,
            TopicName = x.Topic.Name,
            Judge = x.Judge,
            ProblemType = x.ProblemType,
            Difficulty = x.Difficulty,
            Rating = x.Rating,
            ProblemUrl = x.ProblemUrl,
            IsPublished = x.IsPublished,
            Tags = x.Tags
        };
    }

    public async Task<IEnumerable<ProblemDto>> GetByTopicAsync(Guid topicId)
    {
        var data = await _repository.GetByTopicAsync(topicId);

        return data.Select(x => new ProblemDto
        {
            Id = x.Id,
            Title = x.Title,
            Slug = x.Slug,
            TopicId = x.TopicId,
            TopicName = x.Topic.Name,
            Judge = x.Judge,
            ProblemType = x.ProblemType,
            Difficulty = x.Difficulty,
            Rating = x.Rating,
            ProblemUrl = x.ProblemUrl,
            IsPublished = x.IsPublished,
            Tags = x.Tags
        });
    }

    public async Task<Guid> CreateAsync(CreateProblemDto dto)
    {
        var entity = new ProblemEntity
        {
            Title = dto.Title,
            Slug = dto.Slug,
            TopicId = dto.TopicId,
            Judge = dto.Judge,
            ProblemType = dto.ProblemType,
            Difficulty = dto.Difficulty,
            Rating = dto.Rating,
            ProblemUrl = dto.ProblemUrl,
            Tags = dto.Tags
        };

        await _repository.AddAsync(entity);
        await _repository.SaveChangesAsync();

        return entity.Id;
    }

    public async Task<bool> UpdateAsync(UpdateProblemDto dto)
    {
        var entity = await _repository.GetByIdAsync(dto.Id);

        if (entity == null)
            return false;

        entity.Title = dto.Title;
        entity.Slug = dto.Slug;
        entity.TopicId = dto.TopicId;
        entity.Judge = dto.Judge;
        entity.ProblemType = dto.ProblemType;
        entity.Difficulty = dto.Difficulty;
        entity.Rating = dto.Rating;
        entity.ProblemUrl = dto.ProblemUrl;
        entity.IsPublished = dto.IsPublished;
        entity.Tags = dto.Tags;
        entity.UpdatedAt = DateTime.UtcNow;

        await _repository.UpdateAsync(entity);
        await _repository.SaveChangesAsync();

        return true;
    }

    public async Task<bool> DeleteAsync(Guid id)
    {
        var entity = await _repository.GetByIdAsync(id);

        if (entity == null)
            return false;

        await _repository.DeleteAsync(entity);
        await _repository.SaveChangesAsync();

        return true;
    }
}