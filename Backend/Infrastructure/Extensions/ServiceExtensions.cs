using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Infrastructure.Settings;
using MongoDB.Driver;

namespace Infrastructure.Extensions;
public static class ServiceExtensions
{
    public static IServiceCollection AddMongoDb(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<MongoDbSettings>(configuration.GetSection("MongoDbSettings"));
        services.AddSingleton<IMongoClient>(sp => 
            new MongoClient(configuration["MongoDbSettings:ConnectionString"]));
        return services;
    }
}