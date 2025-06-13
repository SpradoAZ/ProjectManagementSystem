using Domain.Entities;
using Domain.Interfaces;
using Application.DTOs;

namespace Application.Services;
public class AuthService
{
    private readonly IUserRepository _userRepository;
    public AuthService(IUserRepository userRepository) => _userRepository = userRepository;

    public async Task<UserResponseDTO> Register(RegisterUserDTO userDto)
    {
        var user = new User
        {
            Name = userDto.Name,
            Email = userDto.Email,
            PasswordHash = BCrypt.Net.BCrypt.HashPassword(userDto.Password)
        };
        await _userRepository.AddAsync(user);
        return new UserResponseDTO(user.Id, user.Name, user.Email);
    }

    public async Task<User> ValidateUser(LoginUserDTO loginDto)
    {
        var user = await _userRepository.GetByEmailAsync(loginDto.Email);
        if (user == null || !BCrypt.Net.BCrypt.Verify(loginDto.Password, user.PasswordHash))
            throw new UnauthorizedAccessException("Credenciales inválidas");
        return user;
    }
}