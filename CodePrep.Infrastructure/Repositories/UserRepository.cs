using CodePrep.Application.Interfaces;
using CodePrep.Domain.Entities;
using CodePrep.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace CodePrep.Infrastructure.Repositories;

public class UserRepository : IUserRepository
{
    private readonly ApplicationDbContext _context;

    public UserRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task AddAsync(User user)
    {
        await _context.Users.AddAsync(user);
        await _context.SaveChangesAsync(); // Auto save kore dibe jate stream ba connection pending na thake
    }

    public async Task<User?> GetByEmailAsync(string email)
    {
        return await _context.Users
            .AsNoTracking() // Npgsql stream read error avoid korar jonno
            .FirstOrDefaultAsync(x => x.Email == email);
    }

    public async Task SaveChangesAsync()
    {
        await _context.SaveChangesAsync();
    }

    public async Task<User?> GetByUserNameAsync(string username)
    {
        return await _context.Users
            .AsNoTracking() // Npgsql stream read error avoid korar jonno
            .FirstOrDefaultAsync(x => x.UserName == username);
    }
}