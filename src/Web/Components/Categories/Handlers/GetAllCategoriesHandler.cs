// =======================================================
// Copyright (c) 2025. All rights reserved.
// File Name :     GetAllCategoriesHandler.cs
// Company :       ArticleManagementApp
// Author :        GitHub Copilot
// Solution Name : ArticleManagementApp
// Project Name :  Web
// =======================================================

namespace Web.Components.Categories.Handlers;

/// <summary>
/// Handler for retrieving all categories with optional filtering.
/// </summary>
public sealed class GetAllCategoriesHandler
{
	private readonly ICategoryRepository _repository;
	private readonly ILogger<GetAllCategoriesHandler> _logger;

	/// <summary>
	/// Initializes a new instance of the <see cref="GetAllCategoriesHandler"/> class.
	/// </summary>
	public GetAllCategoriesHandler(
		ICategoryRepository repository,
		ILogger<GetAllCategoriesHandler> logger)
	{
		_repository = repository;
		_logger = logger;
	}

	/// <summary>
	/// Handles the retrieval of all categories.
	/// </summary>
	/// <param name="activeOnly">If true, only active categories are returned.</param>
	/// <param name="cancellationToken">The cancellation token.</param>
	/// <returns>A result containing the list of categories or an error message.</returns>
	public async Task<Result<List<Category>>> HandleAsync(
		bool activeOnly = false,
		CancellationToken cancellationToken = default)
	{
		try
		{
			var categories = await _repository.GetAllAsync(activeOnly, cancellationToken);
			_logger.LogInformation("GetAllCategories: Retrieved {Count} categories (active only: {ActiveOnly})",
				categories.Count, activeOnly);
			return Result<List<Category>>.Success(categories);
		}
		catch (Exception ex)
		{
			_logger.LogError(ex, "GetAllCategories: Error retrieving categories");
			return Result<List<Category>>.Failure($"Error retrieving categories: {ex.Message}");
		}
	}
}
