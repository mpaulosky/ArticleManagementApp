// =======================================================
// Copyright (c) 2025. All rights reserved.
// File Name :     GetCategoryByIdHandler.cs
// Company :       ArticleManagementApp
// Author :        GitHub Copilot
// Solution Name : ArticleManagementApp
// Project Name :  Web
// =======================================================

namespace Web.Components.Categories.Handlers;

/// <summary>
/// Handler for retrieving a single category by its ID.
/// </summary>
public sealed class GetCategoryByIdHandler
{
	private readonly ICategoryRepository _repository;
	private readonly ILogger<GetCategoryByIdHandler> _logger;

	/// <summary>
	/// Initializes a new instance of the <see cref="GetCategoryByIdHandler"/> class.
	/// </summary>
	public GetCategoryByIdHandler(
		ICategoryRepository repository,
		ILogger<GetCategoryByIdHandler> logger)
	{
		_repository = repository;
		_logger = logger;
	}

	/// <summary>
	/// Handles the retrieval of a category by its ID.
	/// </summary>
	/// <param name="categoryId">The ID of the category to retrieve.</param>
	/// <param name="cancellationToken">The cancellation token.</param>
	/// <returns>A result containing the category or an error message.</returns>
	public async Task<Result<Category?>> HandleAsync(string categoryId, CancellationToken cancellationToken = default)
	{
		if (string.IsNullOrWhiteSpace(categoryId))
		{
			_logger.LogWarning("GetCategoryById: Category ID cannot be null or empty");
			return Result<Category?>.Failure("Category ID is required");
		}

		try
		{
			var category = await _repository.GetByIdAsync(categoryId, cancellationToken);
			if (category is null)
			{
				_logger.LogInformation("GetCategoryById: Category {CategoryId} not found", categoryId);
				return Result<Category?>.Failure($"Category with ID {categoryId} not found");
			}

			_logger.LogInformation("GetCategoryById: Category {CategoryId} retrieved successfully", categoryId);
			return Result<Category?>.Success(category);
		}
		catch (Exception ex)
		{
			_logger.LogError(ex, "GetCategoryById: Error retrieving category {CategoryId}", categoryId);
			return Result<Category?>.Failure($"Error retrieving category: {ex.Message}");
		}
	}
}
