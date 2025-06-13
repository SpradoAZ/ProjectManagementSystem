namespace Domain.Interfaces;

using Domain.Entities;

public interface IProjectRepository
{
    Task<Project> GetByIdAsync(string id);
    Task<IEnumerable<Project>> GetAllByUserIdAsync(string userId);
    Task AddAsync(Project project);
    Task UpdateAsync(Project project);
    Task DeleteAsync(string id);
}