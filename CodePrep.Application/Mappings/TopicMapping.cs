using CodePrep.Application.DTOs.Topic;
using CodePrep.Application.DTOs.Topics;
using CodePrep.Application.Topics.DTOs;
using CodePrep.Domain.Entities;
using Mapster;

namespace CodePrep.Application.Mapping;

public class TopicMapping : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<Topic, TopicResponseDto>();

        config.NewConfig<CreateTopicRequestDto, Topic>();
    }
}