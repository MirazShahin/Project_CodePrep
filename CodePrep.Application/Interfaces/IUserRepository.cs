using CodePrep.Domain.Entities;

namespace CodePrep.Application.Interfaces;

public interface IUserRepository
{
    Task<User?> GetByEmailAsync(string email);

    Task<User?> GetByUserNameAsync(string username);

    Task AddAsync(User user);

    Task SaveChangesAsync();
}