namespace CodePrep.Domain.Enums;

public enum SubmissionStatus
{
    Pending = 0,
    Running = 1,
    Accepted = 2,
    WrongAnswer = 3,
    TimeLimitExceeded = 4,
    RuntimeError = 5,
    CompilationError = 6,
    MemoryLimitExceeded = 7
}