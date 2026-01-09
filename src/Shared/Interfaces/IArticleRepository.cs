using ArticleManagementApp.Shared.Abstractions;
using ArticleManagementApp.Shared.Entities;

namespace ArticleManagementApp.Shared.Interfaces;

/// <summary>
/// Repository interface for Article entity operations.
/// </summary>
public interface IArticleRepository
{
    /// <summary>
    /// Gets all articles with optional filtering.
    /// </summary>
    /// <param name="isPublishedOnly">If true, returns only published articles.</param>
    /// <param name="categoryId">Optional category filter.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>List of articles.</returns>
    Task<List<Article>> GetAllAsync(bool isPublishedOnly = false, string? categoryId = null, CancellationToken cancellationToken = default);

    /// <summary>
    /// Gets an article by ID.
    /// </summary>
    /// <param name="id">The article ID.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>The article or null if not found.</returns>
    Task<Article?> GetByIdAsync(string id, CancellationToken cancellationToken = default);

    /// <summary>
    /// Gets an article by its slug.
    /// </summary>
    /// <param name="slug">The article slug.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>The article or null if not found.</returns>
    Task<Article?> GetBySlugAsync(string slug, CancellationToken cancellationToken = default);

    /// <summary>
    /// Gets articles by category.
    /// </summary>
    /// <param name="categoryId">The category ID.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>List of articles in the category.</returns>
    Task<List<Article>> GetByCategoryAsync(string categoryId, CancellationToken cancellationToken = default);

    /// <summary>
    /// Searches articles by title or content.
    /// </summary>
    /// <param name="query">The search query.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>List of matching articles.</returns>
    Task<List<Article>> SearchAsync(string query, CancellationToken cancellationToken = default);

    /// <summary>
    /// Creates a new article.
    /// </summary>
    /// <param name="article">The article to create.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>Result containing the created article.</returns>
    Task<Result<Article>> CreateAsync(Article article, CancellationToken cancellationToken = default);

    /// <summary>
    /// Updates an existing article.
    /// </summary>
    /// <param name="article">The article to update.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>Result containing the updated article.</returns>
    Task<Result<Article>> UpdateAsync(Article article, CancellationToken cancellationToken = default);

    /// <summary>
    /// Deletes an article by ID.
    /// </summary>
    /// <param name="id">The article ID.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>Result of the deletion.</returns>
    Task<Result> DeleteAsync(string id, CancellationToken cancellationToken = default);

    /// <summary>
    /// Increments the view count for an article.
    /// </summary>
    /// <param name="id">The article ID.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>Result of the operation.</returns>
    Task<Result> IncrementViewCountAsync(string id, CancellationToken cancellationToken = default);

    /// <summary>
    /// Gets the count of articles in a category.
    /// </summary>
    /// <param name="categoryId">The category ID.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>The count of articles.</returns>
    Task<int> GetCountByCategoryAsync(string categoryId, CancellationToken cancellationToken = default);
}
