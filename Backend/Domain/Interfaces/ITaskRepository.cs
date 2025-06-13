namespace Domain.Interfaces;

using Domain.Entities;

public interface ITaskRepository
{
    Task<TaskItem> GetByIdAsync(string id);
    Task<IEnumerable<TaskItem>> GetAllByProjectIdAsync(string projectId);
    Task AddAsync(TaskItem task);
    Task UpdateAsync(TaskItem task);
    Task DeleteAsync(string id);
}