using CodePrep.Application.DTOs.Execution;

namespace CodePrep.Application.Interfaces;

public interface IExecutionService
{
    Task<ExecuteCodeResponseDto> ExecuteAsync(
        ExecuteCodeRequestDto request);
}