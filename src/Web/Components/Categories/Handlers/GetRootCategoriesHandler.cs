// =======================================================
// Copyright (c) 2025. All rights reserved.
// File Name :     GetRootCategoriesHandler.cs
// Company :       ArticleManagementApp
// Author :        GitHub Copilot
// Solution Name : ArticleManagementApp
// Project Name :  Web
// =======================================================

namespace Web.Components.Categories.Handlers;

/// <summary>
/// Handler for retrieving root-level categories (categories with no parent).
/// </summary>
public sealed class GetRootCategoriesHandler
{
	private readonly ICategoryRepository _repository;
	private readonly ILogger<GetRootCategoriesHandler> _logger;

	/// <summary>
	/// Initializes a new instance of the <see cref="GetRootCategoriesHandler"/> class.
	/// </summary>
	public GetRootCategoriesHandler(
		ICategoryRepository repository,
		ILogger<GetRootCategoriesHandler> logger)
	{
		_repository = repository;
		_logger = logger;
	}

	/// <summary>
	/// Handles the retrieval of root-level categories.
	/// </summary>
	/// <param name="cancellationToken">The cancellation token.</param>
	/// <returns>A result containing the list of root categories or an error message.</returns>
	public async Task<Result<List<Category>>> HandleAsync(CancellationToken cancellationToken = default)
	{
		try
		{
			var rootCategories = await _repository.GetRootCategoriesAsync(cancellationToken);
			_logger.LogInformation("GetRootCategories: Retrieved {Count} root categories", rootCategories.Count);
			return Result<List<Category>>.Success(rootCategories);
		}
		catch (Exception ex)
		{
			_logger.LogError(ex, "GetRootCategories: Error retrieving root categories");
			return Result<List<Category>>.Failure($"Error retrieving root categories: {ex.Message}");
		}
	}
}
