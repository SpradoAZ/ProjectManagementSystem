using Domain.Interfaces;
using Domain.Entities;
using Infrastructure.Settings;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace Infrastructure.Repositories
{
    public class ProjectRepository : IProjectRepository
    {
        private readonly IMongoCollection<Project> _projects;

        public ProjectRepository(IMongoClient client, IOptions<MongoDbSettings> settings)
        {
            var database = client.GetDatabase(settings.Value.DatabaseName);
            _projects = database.GetCollection<Project>("projects");
        }

        public async Task<Project> GetByIdAsync(string id)
        {
            return await _projects.Find(p => p.Id == id).FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<Project>> GetAllByUserIdAsync(string userId)
        {
            return await _projects.Find(p => p.OwnerId == userId).ToListAsync();
        }

        public async Task AddAsync(Project project)
        {
            await _projects.InsertOneAsync(project);
        }

        public async Task UpdateAsync(Project project)
        {
            var filter = Builders<Project>.Filter.Eq(p => p.Id, project.Id);
            await _projects.ReplaceOneAsync(filter, project);
        }

        public async Task DeleteAsync(string id)
        {
            await _projects.DeleteOneAsync(p => p.Id == id);
        }
    }
}
