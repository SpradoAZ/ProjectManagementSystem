using Microsoft.AspNetCore.Mvc;
using Application.DTOs;
using Application.Services;
using Domain.Entities;
using Infrastructure.Services;

namespace API.Controllers;
[ApiController]
[Route("api/auth")]
public class AuthController : ControllerBase
{
    private readonly AuthService _authService;
    private readonly JwtService _jwtService;
    public AuthController(AuthService authService, JwtService jwtService)
    {
        _authService = authService;
        _jwtService = jwtService;
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register(RegisterUserDTO registerDto)
    {
        var userResponse = await _authService.Register(registerDto);
        return Ok(userResponse);
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginUserDTO loginDto)
    {
        var user = await _authService.ValidateUser(loginDto);
        var token = _jwtService.GenerateToken(user);
        return Ok(new { token });
    }

}