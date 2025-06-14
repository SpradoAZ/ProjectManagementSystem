using Domain.Entities;
using Domain.Interfaces;
using Application.DTOs;

namespace Application.Services;

public class ProjectService : IProjectService
{
    private readonly IProjectRepository _projectRepository;

    public ProjectService(IProjectRepository projectRepository)
    {
        _projectRepository = projectRepository;
    }

    public async Task<ProjectResponseDTO> CreateProject(CreateProjectDTO projectDto, string ownerId)
    {
        var project = new Project
        {
            Name = projectDto.Name,
            Description = projectDto.Description,
            OwnerId = ownerId
        };

        await _projectRepository.AddAsync(project);
        return new ProjectResponseDTO(project.Id, project.Name, project.Description, project.CreatedAt);
    }

    public async Task<IEnumerable<ProjectResponseDTO>> GetProjectsByUser(string userId)
    {
        var projects = await _projectRepository.GetAllByUserIdAsync(userId);
        return projects.Select(p => new ProjectResponseDTO(p.Id, p.Name, p.Description, p.CreatedAt));
    }

    public async Task<ProjectResponseDTO?> GetByIdAsync(string id)
    {
        var project = await _projectRepository.GetByIdAsync(id);
        if (project == null) return null;

        return new ProjectResponseDTO(project.Id, project.Name, project.Description, project.CreatedAt);
    }

}

// Interface adicional (opcional, pero recomendada para SOLID)
public interface IProjectService
{
    Task<ProjectResponseDTO> CreateProject(CreateProjectDTO projectDto, string ownerId);
    Task<IEnumerable<ProjectResponseDTO>> GetProjectsByUser(string userId);
    Task<ProjectResponseDTO?> GetByIdAsync(string id);

}