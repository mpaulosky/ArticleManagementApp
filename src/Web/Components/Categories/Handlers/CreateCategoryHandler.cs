// =======================================================
// Copyright (c) 2025. All rights reserved.
// File Name :     CreateCategoryHandler.cs
// Company :       ArticleManagementApp
// Author :        GitHub Copilot
// Solution Name : ArticleManagementApp
// Project Name :  Web
// =======================================================

namespace Web.Components.Categories.Handlers;

/// <summary>
/// Handler for creating a new category with validation and persistence.
/// </summary>
public sealed class CreateCategoryHandler
{
	private readonly ICategoryRepository _repository;
	private readonly IValidator<Category> _validator;
	private readonly ILogger<CreateCategoryHandler> _logger;

	/// <summary>
	/// Initializes a new instance of the <see cref="CreateCategoryHandler"/> class.
	/// </summary>
	public CreateCategoryHandler(
		ICategoryRepository repository,
		IValidator<Category> validator,
		ILogger<CreateCategoryHandler> logger)
	{
		_repository = repository;
		_validator = validator;
		_logger = logger;
	}

	/// <summary>
	/// Handles the creation of a new category.
	/// </summary>
	/// <param name="category">The category to create.</param>
	/// <param name="cancellationToken">The cancellation token.</param>
	/// <returns>A result containing the created category or an error message.</returns>
	public async Task<Result<Category>> HandleAsync(Category category, CancellationToken cancellationToken = default)
	{
		if (category is null)
		{
			_logger.LogWarning("CreateCategory: Category cannot be null");
			return Result<Category>.Failure("Category cannot be null");
		}

		// Validate the category
		var validationResult = await _validator.ValidateAsync(category, cancellationToken);
		if (!validationResult.IsValid)
		{
			var errors = string.Join(", ", validationResult.Errors.Select(e => e.ErrorMessage));
			_logger.LogWarning("CreateCategory: Validation failed. Errors: {Errors}", errors);
			return Result<Category>.Failure(errors);
		}

		// Set creation timestamp
		category.CreatedAt = DateTime.UtcNow;
		category.UpdatedAt = DateTime.UtcNow;

		// Create the category in the repository
		var result = await _repository.CreateAsync(category, cancellationToken);

		if (result.IsSuccess)
		{
			_logger.LogInformation("CreateCategory: Category created successfully with ID: {CategoryId}", category.Id);
		}
		else
		{
			_logger.LogError("CreateCategory: Failed to create category. Error: {Error}", result.Error);
		}

		return result;
	}
}
