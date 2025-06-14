using Microsoft.AspNetCore.Mvc;
using Application.DTOs;
using Application.Services;
using Microsoft.AspNetCore.Authorization;

namespace API.Controllers;

[Authorize]
[ApiController]
[Route("api/tasks")]
public class TasksController : ControllerBase
{
    private readonly TaskService _taskService;
    public TasksController(TaskService taskService) => _taskService = taskService;

    [HttpPost]
    public async Task<IActionResult> Create(CreateTaskDTO taskDto)
    {
        var task = await _taskService.CreateTask(taskDto);
        return Ok(task);
    }

    [HttpGet("project/{projectId}")]
    public async Task<IActionResult> GetByProject(string projectId)
    {
        var tasks = await _taskService.GetTasksByProject(projectId);
        return Ok(tasks);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(string id, [FromBody] UpdateTaskDTO dto)
    {
        var updatedTask = await _taskService.UpdateTaskAsync(id, dto);
        if (updatedTask == null) return NotFound();

        return Ok(updatedTask);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(string id)
    {
        var success = await _taskService.DeleteTaskAsync(id);
        if (!success) return NotFound();

        return NoContent();
    }
}