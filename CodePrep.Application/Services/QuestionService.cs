using CodePrep.Application.Common.Exceptions;
using CodePrep.Application.DTOs.Question;
using CodePrep.Application.Interfaces;
using CodePrep.Domain.Entities;

namespace CodePrep.Application.Services;

public class QuestionService : IQuestionService
{
    private readonly IQuestionRepository _questionRepository;

    public QuestionService(IQuestionRepository questionRepository)
    {
        _questionRepository = questionRepository;
    }
    public async Task<QuestionResponseDto> CreateAsync(CreateQuestionRequestDto request)
    {
        var question = new Question
        {
            Title = request.Title,
            Description = request.Description,
            Difficulty = request.Difficulty,
            InputFormat = request.InputFormat,
            OutputFormat = request.OutputFormat,
            Constraints = request.Constraints,
            SampleInput = request.SampleInput,
            SampleOutput = request.SampleOutput,
            Explanation = request.Explanation,
            IsPublished = false
        };

        await _questionRepository.AddAsync(question);
        await _questionRepository.SaveChangesAsync();

        return new QuestionResponseDto
        {
            Id = question.Id,
            Title = question.Title,
            Description = question.Description,
            Difficulty = question.Difficulty,
            IsPublished = question.IsPublished
        };
    }
    public async Task<List<QuestionListDto>> GetAllAsync()
    {
        var questions = await _questionRepository.GetAllAsync();

        return questions.Select(q => new QuestionListDto
        {
            Id = q.Id,
            Title = q.Title,
            Difficulty = q.Difficulty
        }).ToList();
    }

    public async Task<QuestionResponseDto?> GetByIdAsync(Guid id)
    {
        var question = await _questionRepository.GetByIdAsync(id);

        if (question == null)
        {
            throw new NotFoundException("Question not found.");
        }

        return new QuestionResponseDto
        {
            Id = question.Id,
            Title = question.Title,
            Description = question.Description,
            Difficulty = question.Difficulty,
            IsPublished = question.IsPublished
        };
    }

    public async Task UpdateAsync(Guid id, UpdateQuestionRequestDto request)
    {
        var question = await _questionRepository.GetByIdAsync(id);

        if (question == null)
        {
            throw new NotFoundException("Question not found.");
        }

        question.Title = request.Title;
        question.Description = request.Description;
        question.Difficulty = request.Difficulty;
        question.InputFormat = request.InputFormat;
        question.OutputFormat = request.OutputFormat;
        question.Constraints = request.Constraints;
        question.SampleInput = request.SampleInput;
        question.SampleOutput = request.SampleOutput;
        question.Explanation = request.Explanation;

        await _questionRepository.UpdateAsync(question);
        await _questionRepository.SaveChangesAsync();
    }

    public async Task DeleteAsync(Guid id)
    {
        var question = await _questionRepository.GetByIdAsync(id);

        if (question == null)
        {
            throw new NotFoundException("Question not found.");
        }

        await _questionRepository.DeleteAsync(question);
        await _questionRepository.SaveChangesAsync();
    }
}