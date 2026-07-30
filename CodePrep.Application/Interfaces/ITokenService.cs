using CodePrep.Domain.Entities;

namespace CodePrep.Application.Interfaces;

public interface ITokenService
{
    string GenerateToken(User user);
}