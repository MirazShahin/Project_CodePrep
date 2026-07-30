using CodePrep.Application.DTOs.Question;
using CodePrep.Domain.Entities;
using Mapster;

namespace CodePrep.Application.Mapping;

public class QuestionMapping : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<Question, QuestionResponseDto>();

        config.NewConfig<CreateQuestionRequestDto, Question>();
    }
}