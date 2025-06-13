using Domain.Interfaces;
using Domain.Entities;
using Infrastructure.Settings;

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

        
    }
}