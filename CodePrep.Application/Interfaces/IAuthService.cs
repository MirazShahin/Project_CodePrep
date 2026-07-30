using CodePrep.Application.DTOs.Auth;
using System.Security.Claims;

namespace CodePrep.Application.Interfaces;

public interface IAuthService
{
    CurrentUserDto GetCurrentUser(ClaimsPrincipal user);
    Task RegisterAsync(RegisterRequestDto request);
    Task<LoginResponseDto> LoginAsync(LoginRequestDto request);
}
