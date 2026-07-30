using CodePrep.Application.Common.Exceptions;
using CodePrep.Application.DTOs.Auth;
using CodePrep.Application.Interfaces;
using CodePrep.Domain.Entities;
using System.Security.Claims;

namespace CodePrep.Application.Services;

public class AuthService : IAuthService
{
    private readonly IUserRepository _userRepository;
    private readonly ITokenService _tokenService;

    public AuthService(
        IUserRepository userRepository,
        ITokenService tokenService)
    {
        _userRepository = userRepository;
        _tokenService = tokenService;
    }

    public async Task RegisterAsync(RegisterRequestDto request)
    {
        var emailExists = await _userRepository.GetByEmailAsync(request.Email);
        var usernameExists = await _userRepository.GetByUserNameAsync(request.UserName);

        if (emailExists != null)
            throw new DuplicateException("Email already exists.");

        if (usernameExists != null)
            throw new DuplicateException("Username already exists.");

        var user = new User
        {
            FirstName = request.FirstName,
            LastName = request.LastName,
            UserName = request.UserName,
            Email = request.Email,
            PasswordHash = BCrypt.Net.BCrypt.HashPassword(request.Password)
        };

        await _userRepository.AddAsync(user);
        await _userRepository.SaveChangesAsync();
    }

    public async Task<LoginResponseDto> LoginAsync(LoginRequestDto request)
    {
        var user = await _userRepository.GetByEmailAsync(request.Email);

        if (user == null)
            throw new UnauthorizedException("Invalid email or password.");

        bool isPasswordValid = BCrypt.Net.BCrypt.Verify(
            request.Password,
            user.PasswordHash);

        if (!isPasswordValid)
            throw new UnauthorizedException("Invalid email or password.");

        var token = _tokenService.GenerateToken(user);

        return new LoginResponseDto
        {
            Token = token
        };
    }
    public CurrentUserDto GetCurrentUser(ClaimsPrincipal user)
    {
        return new CurrentUserDto
        {
            Id = Guid.Parse(
                user.FindFirst(ClaimTypes.NameIdentifier)!.Value),

            UserName =
                user.FindFirst(ClaimTypes.Name)!.Value,

            Email =
                user.FindFirst(ClaimTypes.Email)!.Value,

            Role =
                user.FindFirst(ClaimTypes.Role)!.Value
        };
    }
}