// =======================================================
// Copyright (c) 2025. All rights reserved.
// File Name :     UpdateCategoryHandler.cs
// Company :       ArticleManagementApp
// Author :        GitHub Copilot
// Solution Name : ArticleManagementApp
// Project Name :  Web
// =======================================================

namespace Web.Components.Categories.Handlers;

/// <summary>
/// Handler for updating an existing category with validation and persistence.
/// </summary>
public sealed class UpdateCategoryHandler
{
	private readonly ICategoryRepository _repository;
	private readonly IValidator<Category> _validator;
	private readonly ILogger<UpdateCategoryHandler> _logger;

	/// <summary>
	/// Initializes a new instance of the <see cref="UpdateCategoryHandler"/> class.
	/// </summary>
	public UpdateCategoryHandler(
		ICategoryRepository repository,
		IValidator<Category> validator,
		ILogger<UpdateCategoryHandler> logger)
	{
		_repository = repository;
		_validator = validator;
		_logger = logger;
	}

	/// <summary>
	/// Handles the update of an existing category.
	/// </summary>
	/// <param name="category">The category with updated values.</param>
	/// <param name="cancellationToken">The cancellation token.</param>
	/// <returns>A result containing the updated category or an error message.</returns>
	public async Task<Result<Category>> HandleAsync(Category category, CancellationToken cancellationToken = default)
	{
		if (category is null)
		{
			_logger.LogWarning("UpdateCategory: Category cannot be null");
			return Result<Category>.Failure("Category cannot be null");
		}

		if (string.IsNullOrEmpty(category.Id))
		{
			_logger.LogWarning("UpdateCategory: Category ID cannot be null or empty");
			return Result<Category>.Failure("Category ID is required");
		}

		// Validate the category
		var validationResult = await _validator.ValidateAsync(category, cancellationToken);
		if (!validationResult.IsValid)
		{
			var errors = string.Join(", ", validationResult.Errors.Select(e => e.ErrorMessage));
			_logger.LogWarning("UpdateCategory: Validation failed for category {CategoryId}. Errors: {Errors}",
				category.Id, errors);
			return Result<Category>.Failure(errors);
		}

		// Update the modified timestamp
		category.UpdatedAt = DateTime.UtcNow;

		// Update the category in the repository
		var result = await _repository.UpdateAsync(category, cancellationToken);

		if (result.IsSuccess)
		{
			_logger.LogInformation("UpdateCategory: Category {CategoryId} updated successfully", category.Id);
		}
		else
		{
			_logger.LogError("UpdateCategory: Failed to update category {CategoryId}. Error: {Error}",
				category.Id, result.Error);
		}

		return result;
	}
}
