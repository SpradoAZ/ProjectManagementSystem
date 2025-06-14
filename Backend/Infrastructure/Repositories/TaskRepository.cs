using Domain.Entities;
using Domain.Interfaces;
using Infrastructure.Settings;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace Infrastructure.Repositories
{
    public class TaskRepository : ITaskRepository
    {
        private readonly IMongoCollection<TaskItem> _tasks;

        public TaskRepository(IMongoClient client, IOptions<MongoDbSettings> settings)
        {
            var database = client.GetDatabase(settings.Value.DatabaseName);
            _tasks = database.GetCollection<TaskItem>("tasks");
        }

        public async Task<TaskItem> GetByIdAsync(string id)
        {
            return await _tasks.Find(t => t.Id == id).FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<TaskItem>> GetAllByProjectIdAsync(string projectId)
        {
            return await _tasks.Find(t => t.ProjectId == projectId).ToListAsync();
        }

        public async Task AddAsync(TaskItem task)
        {
            await _tasks.InsertOneAsync(task);
        }

        public async Task UpdateAsync(TaskItem task)
        {
            var filter = Builders<TaskItem>.Filter.Eq(t => t.Id, task.Id);
            await _tasks.ReplaceOneAsync(filter, task);
        }

        public async Task DeleteAsync(string id)
        {
            await _tasks.DeleteOneAsync(t => t.Id == id);
        }
    }
}
