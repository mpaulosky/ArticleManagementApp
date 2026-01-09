// =======================================================
// Copyright (c) 2025. All rights reserved.
// File Name :     GetSubcategoriesHandler.cs
// Company :       ArticleManagementApp
// Author :        GitHub Copilot
// Solution Name : ArticleManagementApp
// Project Name :  Web
// =======================================================

namespace Web.Components.Categories.Handlers;

/// <summary>
/// Handler for retrieving subcategories of a given parent category.
/// </summary>
public sealed class GetSubcategoriesHandler
{
	private readonly ICategoryRepository _repository;
	private readonly ILogger<GetSubcategoriesHandler> _logger;

	/// <summary>
	/// Initializes a new instance of the <see cref="GetSubcategoriesHandler"/> class.
	/// </summary>
	public GetSubcategoriesHandler(
		ICategoryRepository repository,
		ILogger<GetSubcategoriesHandler> logger)
	{
		_repository = repository;
		_logger = logger;
	}

	/// <summary>
	/// Handles the retrieval of subcategories for a given parent category.
	/// </summary>
	/// <param name="parentCategoryId">The ID of the parent category.</param>
	/// <param name="cancellationToken">The cancellation token.</param>
	/// <returns>A result containing the list of subcategories or an error message.</returns>
	public async Task<Result<List<Category>>> HandleAsync(
		string parentCategoryId,
		CancellationToken cancellationToken = default)
	{
		if (string.IsNullOrWhiteSpace(parentCategoryId))
		{
			_logger.LogWarning("GetSubcategories: Parent category ID cannot be null or empty");
			return Result<List<Category>>.Failure("Parent category ID is required");
		}

		try
		{
			var subcategories = await _repository.GetSubcategoriesAsync(parentCategoryId, cancellationToken);
			_logger.LogInformation("GetSubcategories: Retrieved {Count} subcategories for parent {ParentId}",
				subcategories.Count, parentCategoryId);
			return Result<List<Category>>.Success(subcategories);
		}
		catch (Exception ex)
		{
			_logger.LogError(ex, "GetSubcategories: Error retrieving subcategories for parent {ParentId}",
				parentCategoryId);
			return Result<List<Category>>.Failure($"Error retrieving subcategories: {ex.Message}");
		}
	}
}
