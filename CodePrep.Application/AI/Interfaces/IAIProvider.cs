using CodePrep.Application.AI.DTOs;

namespace CodePrep.Application.AI.Interfaces;

public interface IAIProvider
{
    Task<AIResponseDto> GenerateAsync(AIRequestDto request);
    Task<AIResponseDto> GenerateWithSystemInstructionAsync(string systemInstruction, string userPrompt);
}