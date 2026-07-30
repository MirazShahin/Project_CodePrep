using CodePrep.Application.Interfaces;
using CodePrep.Domain.Entities;
using CodePrep.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace CodePrep.Infrastructure.Repositories;

public class QuestionRepository : IQuestionRepository
{
    private readonly ApplicationDbContext _context;

    public QuestionRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task AddAsync(Question question)
    {
        await _context.Questions.AddAsync(question);
    }

    public async Task<Question?> GetByIdAsync(Guid id)
    {
        return await _context.Questions.FindAsync(id);
    }

    public async Task<List<Question>> GetAllAsync()
    {
        return await _context.Questions.ToListAsync();
    }

    public Task UpdateAsync(Question question)
    {
        _context.Questions.Update(question);
        return Task.CompletedTask;
    }

    public Task DeleteAsync(Question question)
    {
        _context.Questions.Remove(question);
        return Task.CompletedTask;
    }

    public async Task SaveChangesAsync()
    {
        await _context.SaveChangesAsync();
    }
}