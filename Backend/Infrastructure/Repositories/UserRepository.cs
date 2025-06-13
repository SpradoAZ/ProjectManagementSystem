using Domain.Entities;
using Domain.Interfaces;
using MongoDB.Driver;
using Microsoft.Extensions.Options;
using Infrastructure.Settings;

namespace Infrastructure.Repositories;
public class UserRepository : IUserRepository
{
    private readonly IMongoCollection<User> _users;
    public UserRepository(IMongoClient client, IOptions<MongoDbSettings> settings)
    {
        var database = client.GetDatabase(settings.Value.DatabaseName);
        _users = database.GetCollection<User>("users");
    }

    public async Task<User> GetByIdAsync(string id) =>
        await _users.Find(u => u.Id == id).FirstOrDefaultAsync();

    public async Task<User> GetByEmailAsync(string email) =>
        await _users.Find(u => u.Email == email).FirstOrDefaultAsync();

    public async Task AddAsync(User user) =>
        await _users.InsertOneAsync(user);
}