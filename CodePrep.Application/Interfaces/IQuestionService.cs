using CodePrep.Application.DTOs.Question;

namespace CodePrep.Application.Interfaces;

public interface IQuestionService
{
    Task<QuestionResponseDto> CreateAsync(CreateQuestionRequestDto request);

    Task<List<QuestionListDto>> GetAllAsync();

    Task<QuestionResponseDto?> GetByIdAsync(Guid id);

    Task UpdateAsync(Guid id, UpdateQuestionRequestDto request);

    Task DeleteAsync(Guid id);
}