// =======================================================
// Copyright (c) 2025. All rights reserved.
// File Name :     DeleteCategoryHandler.cs
// Company :       ArticleManagementApp
// Author :        GitHub Copilot
// Solution Name : ArticleManagementApp
// Project Name :  Web
// =======================================================

namespace Web.Components.Categories.Handlers;

/// <summary>
/// Handler for deleting a category.
/// </summary>
public sealed class DeleteCategoryHandler
{
	private readonly ICategoryRepository _repository;
	private readonly ILogger<DeleteCategoryHandler> _logger;

	/// <summary>
	/// Initializes a new instance of the <see cref="DeleteCategoryHandler"/> class.
	/// </summary>
	public DeleteCategoryHandler(
		ICategoryRepository repository,
		ILogger<DeleteCategoryHandler> logger)
	{
		_repository = repository;
		_logger = logger;
	}

	/// <summary>
	/// Handles the deletion of a category.
	/// </summary>
	/// <param name="categoryId">The ID of the category to delete.</param>
	/// <param name="cancellationToken">The cancellation token.</param>
	/// <returns>A result indicating success or failure.</returns>
	public async Task<Result> HandleAsync(string categoryId, CancellationToken cancellationToken = default)
	{
		if (string.IsNullOrWhiteSpace(categoryId))
		{
			_logger.LogWarning("DeleteCategory: Category ID cannot be null or empty");
			return Result.Failure("Category ID is required");
		}

		// Delete the category
		var result = await _repository.DeleteAsync(categoryId, cancellationToken);

		if (result.IsSuccess)
		{
			_logger.LogInformation("DeleteCategory: Category {CategoryId} deleted successfully", categoryId);
		}
		else
		{
			_logger.LogError("DeleteCategory: Failed to delete category {CategoryId}. Error: {Error}",
				categoryId, result.Error);
		}

		return result;
	}
}
