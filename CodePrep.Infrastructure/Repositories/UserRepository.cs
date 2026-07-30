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
        await _context.SaveChangesAsync();
    }

    public async Task<User?> GetByEmailAsync(string email)
    {
        // Stream conflict aoratar jonno synchronous execution use kora holo
        return await Task.Run(() => _context.Users
            .AsNoTracking()
            .FirstOrDefault(x => x.Email == email));
    }

    public async Task SaveChangesAsync()
    {
        await _context.SaveChangesAsync();
    }

    public async Task<User?> GetByUserNameAsync(string username)
    {
        return await Task.Run(() => _context.Users
            .AsNoTracking()
            .FirstOrDefault(x => x.UserName == username));
    }
}
