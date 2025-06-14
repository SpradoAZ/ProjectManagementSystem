using Microsoft.AspNetCore.Mvc;
using Application.DTOs;
using Application.Services;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace API.Controllers;
[Authorize]
[ApiController]
[Route("api/projects")]
public class ProjectsController : ControllerBase
{
    private readonly ProjectService _projectService;
    public ProjectsController(ProjectService projectService) => _projectService = projectService;

    [HttpPost]
    public async Task<IActionResult> Create(CreateProjectDTO projectDto)
    {
        var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        var project = await _projectService.CreateProject(projectDto, userId);
        return Ok(project);
    }

    [HttpGet]
    public async Task<IActionResult> GetByUser()
    {
        var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        var projects = await _projectService.GetProjectsByUser(userId);
        return Ok(projects);
    }
}