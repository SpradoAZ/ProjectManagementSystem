namespace Application.DTOs;

public record CreateProjectDTO(string Name, string Description);
public record ProjectResponseDTO(string Id, string Name, string Description, DateTime CreatedAt);