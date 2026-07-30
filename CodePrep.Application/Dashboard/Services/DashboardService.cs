using CodePrep.Application.Dashboard.DTOs;
using CodePrep.Application.Dashboard.Interfaces;
using CodePrep.Application.Interfaces;

namespace CodePrep.Application.Dashboard.Services;

public class DashboardService : IDashboardService
{
    private readonly ITopicRepository _topicRepository;
    private readonly ILearningRepository _learningRepository;
    private readonly IProblemRepository _problemRepository;
    private readonly IInterviewRepository _interviewRepository;
    private readonly IUserProblemRepository _userProblemRepository;

    public DashboardService(
        ITopicRepository topicRepository,
        ILearningRepository learningRepository,
        IProblemRepository problemRepository,
        IInterviewRepository interviewRepository,
        IUserProblemRepository userProblemRepository)
    {
        _topicRepository = topicRepository;
        _learningRepository = learningRepository;
        _problemRepository = problemRepository;
        _interviewRepository = interviewRepository;
        _userProblemRepository = userProblemRepository;
    }

    public async Task<DashboardDto> GetDashboardAsync(Guid userId)
    {
        return new DashboardDto
        {
            TotalTopics = await _topicRepository.CountAsync(),
            TotalLearningContents = await _learningRepository.CountAsync(),
            TotalProblems = await _problemRepository.CountAsync(),
            TotalInterviewQuestions = await _interviewRepository.CountAsync(),
            SolvedProblems = await _userProblemRepository.CountSolvedAsync(userId),

            // আপাতত Quiz বানাইনি
            QuizAttempts = 0
        };
    }
}