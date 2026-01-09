using ArticleManagementApp.Shared.Interfaces;
using ArticleManagementApp.Web.Data;
using MongoDB.Driver;

namespace ArticleManagementApp.Web.Services;

/// <summary>
/// Extension methods for registering data access services.
/// </summary>
public static class DataAccessExtensions
{
    /// <summary>
    /// Adds MongoDB repositories to the service collection.
    /// </summary>
    /// <param name="services">The service collection.</param>
    /// <param name="mongoClient">The MongoDB client.</param>
    /// <param name="databaseName">The database name.</param>
    /// <returns>The service collection.</returns>
    public static IServiceCollection AddMongoDbRepositories(
        this IServiceCollection services,
        IMongoClient mongoClient,
        string databaseName = "ArticleManagementDb")
    {
        // Register repositories as singletons since they are stateless
        services.AddSingleton(mongoClient);
        services.AddSingleton<IArticleRepository>(
            _ => new ArticleRepository(mongoClient, databaseName));
        services.AddSingleton<ICategoryRepository>(
            _ => new CategoryRepository(mongoClient, databaseName));

        return services;
    }
}
