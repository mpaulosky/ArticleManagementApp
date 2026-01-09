using ArticleManagementApp.Shared.Abstractions;
using ArticleManagementApp.Shared.Entities;
using ArticleManagementApp.Shared.Interfaces;
using MongoDB.Driver;

namespace ArticleManagementApp.Web.Data;

/// <summary>
/// MongoDB repository implementation for Category entity.
/// </summary>
public sealed class CategoryRepository : ICategoryRepository
{
    private readonly IMongoCollection<Category> _collection;

    public CategoryRepository(IMongoClient mongoClient, string databaseName = "ArticleManagementDb")
    {
        var database = mongoClient.GetDatabase(databaseName);
        _collection = database.GetCollection<Category>("categories");
    }

    public async Task<List<Category>> GetAllAsync(
        bool activeOnly = false,
        CancellationToken cancellationToken = default)
    {
        var filter = activeOnly
            ? Builders<Category>.Filter.Eq(c => c.IsActive, true)
            : Builders<Category>.Filter.Empty;

        return await _collection
            .Find(filter)
            .SortBy(c => c.DisplayOrder)
            .ToListAsync(cancellationToken);
    }

    public async Task<Category?> GetByIdAsync(string id, CancellationToken cancellationToken = default)
    {
        return await _collection
            .Find(c => c.Id == id)
            .FirstOrDefaultAsync(cancellationToken);
    }

    public async Task<Category?> GetBySlugAsync(string slug, CancellationToken cancellationToken = default)
    {
        return await _collection
            .Find(c => c.Slug == slug)
            .FirstOrDefaultAsync(cancellationToken);
    }

    public async Task<List<Category>> GetSubcategoriesAsync(
        string parentId,
        CancellationToken cancellationToken = default)
    {
        return await _collection
            .Find(c => c.ParentId == parentId && c.IsActive)
            .SortBy(c => c.DisplayOrder)
            .ToListAsync(cancellationToken);
    }

    public async Task<List<Category>> GetRootCategoriesAsync(CancellationToken cancellationToken = default)
    {
        return await _collection
            .Find(c => string.IsNullOrEmpty(c.ParentId) && c.IsActive)
            .SortBy(c => c.DisplayOrder)
            .ToListAsync(cancellationToken);
    }

    public async Task<Result<Category>> CreateAsync(
        Category category,
        CancellationToken cancellationToken = default)
    {
        try
        {
            await _collection.InsertOneAsync(category, cancellationToken: cancellationToken);
            return Result<Category>.Success(category);
        }
        catch (MongoWriteException ex)
        {
            return Result<Category>.Failure($"Failed to create category: {ex.Message}");
        }
    }

    public async Task<Result<Category>> UpdateAsync(
        Category category,
        CancellationToken cancellationToken = default)
    {
        try
        {
            category.UpdatedAt = DateTime.UtcNow;

            var result = await _collection.ReplaceOneAsync(
                c => c.Id == category.Id,
                category,
                cancellationToken: cancellationToken);

            if (result.MatchedCount == 0)
            {
                return Result<Category>.Failure("Category not found.");
            }

            return Result<Category>.Success(category);
        }
        catch (MongoWriteException ex)
        {
            return Result<Category>.Failure($"Failed to update category: {ex.Message}");
        }
    }

    public async Task<Result> DeleteAsync(string id, CancellationToken cancellationToken = default)
    {
        try
        {
            var result = await _collection.DeleteOneAsync(
                c => c.Id == id,
                cancellationToken: cancellationToken);

            if (result.DeletedCount == 0)
            {
                return Result.Failure("Category not found.");
            }

            return Result.Success();
        }
        catch (MongoWriteException ex)
        {
            return Result.Failure($"Failed to delete category: {ex.Message}");
        }
    }

    public async Task<bool> ExistsAsync(string id, CancellationToken cancellationToken = default)
    {
        return await _collection
            .Find(c => c.Id == id)
            .AnyAsync(cancellationToken);
    }

    public async Task<int> GetCountAsync(CancellationToken cancellationToken = default)
    {
        return (int)await _collection.CountDocumentsAsync(
            Builders<Category>.Filter.Empty,
            cancellationToken: cancellationToken);
    }
}
