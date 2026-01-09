using ArticleManagementApp.Shared.Abstractions;
using ArticleManagementApp.Shared.Entities;

namespace ArticleManagementApp.Shared.Interfaces;

/// <summary>
/// Repository interface for Category entity operations.
/// </summary>
public interface ICategoryRepository
{
    /// <summary>
    /// Gets all categories.
    /// </summary>
    /// <param name="activeOnly">If true, returns only active categories.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>List of categories.</returns>
    Task<List<Category>> GetAllAsync(bool activeOnly = false, CancellationToken cancellationToken = default);

    /// <summary>
    /// Gets a category by ID.
    /// </summary>
    /// <param name="id">The category ID.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>The category or null if not found.</returns>
    Task<Category?> GetByIdAsync(string id, CancellationToken cancellationToken = default);

    /// <summary>
    /// Gets a category by its slug.
    /// </summary>
    /// <param name="slug">The category slug.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>The category or null if not found.</returns>
    Task<Category?> GetBySlugAsync(string slug, CancellationToken cancellationToken = default);

    /// <summary>
    /// Gets all subcategories for a parent category.
    /// </summary>
    /// <param name="parentId">The parent category ID.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>List of subcategories.</returns>
    Task<List<Category>> GetSubcategoriesAsync(string parentId, CancellationToken cancellationToken = default);

    /// <summary>
    /// Gets all root categories (those without a parent).
    /// </summary>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>List of root categories.</returns>
    Task<List<Category>> GetRootCategoriesAsync(CancellationToken cancellationToken = default);

    /// <summary>
    /// Creates a new category.
    /// </summary>
    /// <param name="category">The category to create.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>Result containing the created category.</returns>
    Task<Result<Category>> CreateAsync(Category category, CancellationToken cancellationToken = default);

    /// <summary>
    /// Updates an existing category.
    /// </summary>
    /// <param name="category">The category to update.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>Result containing the updated category.</returns>
    Task<Result<Category>> UpdateAsync(Category category, CancellationToken cancellationToken = default);

    /// <summary>
    /// Deletes a category by ID.
    /// </summary>
    /// <param name="id">The category ID.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>Result of the deletion.</returns>
    Task<Result> DeleteAsync(string id, CancellationToken cancellationToken = default);

    /// <summary>
    /// Checks if a category exists by ID.
    /// </summary>
    /// <param name="id">The category ID.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>True if the category exists.</returns>
    Task<bool> ExistsAsync(string id, CancellationToken cancellationToken = default);

    /// <summary>
    /// Gets the total count of categories.
    /// </summary>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>The count of categories.</returns>
    Task<int> GetCountAsync(CancellationToken cancellationToken = default);
}
