namespace Domain.Entities;

public class TaskItem
{
    public string Id { get; set; } = string.Empty;
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string ProjectId { get; set; } = string.Empty;
    public string AssignedUserId { get; set; } = string.Empty;
    public DateTime DueDate { get; set; }
    public bool IsCompleted { get; set; }
    public int Priority { get; set; } // 1: Low, 2: Medium, 3: High
}