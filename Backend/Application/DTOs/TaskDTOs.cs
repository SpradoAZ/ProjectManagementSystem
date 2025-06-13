namespace Application.DTOs;

public record CreateTaskDTO(string Title, string Description, string ProjectId, string AssignedUserId, DateTime DueDate, int Priority);
public record UpdateTaskDTO(string Title, string Description, DateTime DueDate, int Priority, bool IsCompleted);
public record TaskResponseDTO(string Id, string Title, string Description, string ProjectId, string AssignedUserId, DateTime DueDate, bool IsCompleted, int Priority);