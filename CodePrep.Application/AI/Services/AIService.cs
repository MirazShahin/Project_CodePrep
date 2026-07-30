using CodePrep.Application.AI.DTOs;
using CodePrep.Application.AI.Interfaces;

namespace CodePrep.Application.AI.Services;

public class AIService
{
    private readonly IAIProvider _provider;

    public AIService(IAIProvider provider)
    {
        _provider = provider;
    }

    public async Task<AIResponseDto> AskAsync(AIRequestDto request)
    {
        return await _provider.GenerateAsync(request);
    }
}