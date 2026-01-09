using ArticleManagementApp.Shared.Abstractions;
using ArticleManagementApp.Shared.Entities;
using ArticleManagementApp.Shared.Interfaces;
using MongoDB.Driver;

namespace ArticleManagementApp.Web.Data;

/// <summary>
/// MongoDB repository implementation for Article entity.
/// </summary>
public sealed class ArticleRepository : IArticleRepository
{
    private readonly IMongoCollection<Article> _collection;

    public ArticleRepository(IMongoClient mongoClient, string databaseName = "ArticleManagementDb")
    {
        var database = mongoClient.GetDatabase(databaseName);
        _collection = database.GetCollection<Article>("articles");
    }

    public async Task<List<Article>> GetAllAsync(
        bool isPublishedOnly = false,
        string? categoryId = null,
        CancellationToken cancellationToken = default)
    {
        var filter = Builders<Article>.Filter.Empty;

        if (isPublishedOnly)
        {
            filter &= Builders<Article>.Filter.Eq(a => a.IsPublished, true);
        }

        if (!string.IsNullOrWhiteSpace(categoryId))
        {
            filter &= Builders<Article>.Filter.Eq(a => a.CategoryId, categoryId);
        }

        return await _collection
            .Find(filter)
            .SortByDescending(a => a.CreatedAt)
            .ToListAsync(cancellationToken);
    }

    public async Task<Article?> GetByIdAsync(string id, CancellationToken cancellationToken = default)
    {
        return await _collection
            .Find(a => a.Id == id)
            .FirstOrDefaultAsync(cancellationToken);
    }

    public async Task<Article?> GetBySlugAsync(string slug, CancellationToken cancellationToken = default)
    {
        return await _collection
            .Find(a => a.Slug == slug)
            .FirstOrDefaultAsync(cancellationToken);
    }

    public async Task<List<Article>> GetByCategoryAsync(
        string categoryId,
        CancellationToken cancellationToken = default)
    {
        return await _collection
            .Find(a => a.CategoryId == categoryId && a.IsPublished)
            .SortByDescending(a => a.CreatedAt)
            .ToListAsync(cancellationToken);
    }

    public async Task<List<Article>> SearchAsync(
        string query,
        CancellationToken cancellationToken = default)
    {
        var filter = Builders<Article>.Filter.Or(
            Builders<Article>.Filter.Regex(a => a.Title, new MongoDB.Bson.BsonRegularExpression(query, "i")),
            Builders<Article>.Filter.Regex(a => a.Content, new MongoDB.Bson.BsonRegularExpression(query, "i")),
            Builders<Article>.Filter.Regex(a => a.Summary, new MongoDB.Bson.BsonRegularExpression(query, "i"))
        );

        return await _collection
            .Find(filter)
            .SortByDescending(a => a.CreatedAt)
            .ToListAsync(cancellationToken);
    }

    public async Task<Result<Article>> CreateAsync(
        Article article,
        CancellationToken cancellationToken = default)
    {
        try
        {
            await _collection.InsertOneAsync(article, cancellationToken: cancellationToken);
            return Result<Article>.Success(article);
        }
        catch (MongoWriteException ex)
        {
            return Result<Article>.Failure($"Failed to create article: {ex.Message}");
        }
    }

    public async Task<Result<Article>> UpdateAsync(
        Article article,
        CancellationToken cancellationToken = default)
    {
        try
        {
            article.UpdatedAt = DateTime.UtcNow;

            var result = await _collection.ReplaceOneAsync(
                a => a.Id == article.Id,
                article,
                cancellationToken: cancellationToken);

            if (result.MatchedCount == 0)
            {
                return Result<Article>.Failure("Article not found.");
            }

            return Result<Article>.Success(article);
        }
        catch (MongoWriteException ex)
        {
            return Result<Article>.Failure($"Failed to update article: {ex.Message}");
        }
    }

    public async Task<Result> DeleteAsync(string id, CancellationToken cancellationToken = default)
    {
        try
        {
            var result = await _collection.DeleteOneAsync(
                a => a.Id == id,
                cancellationToken: cancellationToken);

            if (result.DeletedCount == 0)
            {
                return Result.Failure("Article not found.");
            }

            return Result.Success();
        }
        catch (MongoWriteException ex)
        {
            return Result.Failure($"Failed to delete article: {ex.Message}");
        }
    }

    public async Task<Result> IncrementViewCountAsync(
        string id,
        CancellationToken cancellationToken = default)
    {
        try
        {
            var update = Builders<Article>.Update.Inc(a => a.ViewCount, 1);

            var result = await _collection.UpdateOneAsync(
                a => a.Id == id,
                update,
                cancellationToken: cancellationToken);

            if (result.MatchedCount == 0)
            {
                return Result.Failure("Article not found.");
            }

            return Result.Success();
        }
        catch (MongoWriteException ex)
        {
            return Result.Failure($"Failed to increment view count: {ex.Message}");
        }
    }

    public async Task<int> GetCountByCategoryAsync(
        string categoryId,
        CancellationToken cancellationToken = default)
    {
        return (int)await _collection.CountDocumentsAsync(
            a => a.CategoryId == categoryId,
            cancellationToken: cancellationToken);
    }
}
