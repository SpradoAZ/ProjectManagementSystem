using Domain.Entities;
using Domain.Interfaces;
using Application.DTOs;

namespace Application.Services;

public class TaskService : ITaskService
{
    private readonly ITaskRepository _taskRepository;

    public TaskService(ITaskRepository taskRepository)
    {
        _taskRepository = taskRepository;
    }

    public async Task<TaskResponseDTO> CreateTask(CreateTaskDTO taskDto)
    {
        var task = new TaskItem
        {
            Title = taskDto.Title,
            Description = taskDto.Description,
            ProjectId = taskDto.ProjectId,
            AssignedUserId = taskDto.AssignedUserId,
            DueDate = taskDto.DueDate,
            Priority = taskDto.Priority,
            IsCompleted = false
        };

        await _taskRepository.AddAsync(task);
        return new TaskResponseDTO(
            task.Id, task.Title, task.Description, 
            task.ProjectId, task.AssignedUserId, 
            task.DueDate, task.IsCompleted, task.Priority
        );
    }

    public async Task<IEnumerable<TaskResponseDTO>> GetTasksByProject(string projectId)
    {
        var tasks = await _taskRepository.GetAllByProjectIdAsync(projectId);
        return tasks.Select(t => new TaskResponseDTO(
            t.Id, t.Title, t.Description,
            t.ProjectId, t.AssignedUserId,
            t.DueDate, t.IsCompleted, t.Priority
        ));
    }
}

// Interface adicional
public interface ITaskService
{
    Task<TaskResponseDTO> CreateTask(CreateTaskDTO taskDto);
    Task<IEnumerable<TaskResponseDTO>> GetTasksByProject(string projectId);
}