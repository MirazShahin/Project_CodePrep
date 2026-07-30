using CodePrep.Application.DTOs.Auth;
using CodePrep.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CodePrep.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;

    public AuthController(IAuthService authService)
    {
        _authService = authService;
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login(LoginRequestDto request)
    {
        var response = await _authService.LoginAsync(request);

        return Ok(new
        {
            success = true,
            message = "Login successful.",
            data = response
        });
    }
    [HttpPost("register")]
    public async Task<IActionResult> Register(RegisterRequestDto request)
    {
        await _authService.RegisterAsync(request);

        return CreatedAtAction(nameof(Register), new
        {
            success = true,
            message = "Registration successful."
        });
    }

    [Authorize]
    [HttpGet("me")]
    public IActionResult Me()
    {
        var user = _authService.GetCurrentUser(User);

        return Ok(new
        {
            success = true,
            data = user
        });
    }
}