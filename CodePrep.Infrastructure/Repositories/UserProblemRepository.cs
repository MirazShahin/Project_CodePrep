using CodePrep.Application.Interfaces;
using CodePrep.Domain.Entities;
using CodePrep.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace CodePrep.Infrastructure.Repositories;

public class UserProblemRepository : IUserProblemRepository
{
    private readonly ApplicationDbContext _context;

    public UserProblemRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<UserProblem?> GetAsync(Guid userId, Guid problemId)
    {
        return await _context.UserProblems
            .FirstOrDefaultAsync(x =>
                x.UserId == userId &&
                x.ProblemId == problemId);
    }

    public async Task<IEnumerable<UserProblem>> GetByUserAsync(Guid userId)
    {
        return await _context.UserProblems
            .Include(x => x.Problem)
            .Where(x => x.UserId == userId)
            .ToListAsync();
    }

    public async Task AddAsync(UserProblem entity)
    {
        await _context.UserProblems.AddAsync(entity);
    }

    public Task UpdateAsync(UserProblem entity)
    {
        _context.UserProblems.Update(entity);
        return Task.CompletedTask;
    }

    public async Task SaveChangesAsync()
    {
        await _context.SaveChangesAsync();
    }

    public async Task<int> CountSolvedAsync(Guid userId)
    {
        return await _context.UserProblems
            .CountAsync(x =>
                x.UserId == userId &&
                x.IsSolved);
    }
}