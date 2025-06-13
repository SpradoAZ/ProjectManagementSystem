namespace Application.DTOs;

public record RegisterUserDTO(string Name, string Email, string Password);
public record LoginUserDTO(string Email, string Password);
public record UserResponseDTO(string Id, string Name, string Email);