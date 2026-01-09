// =======================================================
// Copyright (c) 2025. All rights reserved.
// File Name :     CreateArticleHandler.cs
// Company :       ArticleManagementApp
// Author :        GitHub Copilot
// Solution Name : ArticleManagementApp
// Project Name :  Web
// =======================================================

namespace Web.Components.Articles.Handlers;

/// <summary>
/// Handler for creating a new article with validation and persistence.
/// </summary>
public sealed class CreateArticleHandler
{
	private readonly IArticleRepository _repository;
	private readonly IValidator<Article> _validator;
	private readonly ILogger<CreateArticleHandler> _logger;

	/// <summary>
	/// Initializes a new instance of the <see cref="CreateArticleHandler"/> class.
	/// </summary>
	public CreateArticleHandler(
		IArticleRepository repository,
		IValidator<Article> validator,
		ILogger<CreateArticleHandler> logger)
	{
		_repository = repository;
		_validator = validator;
		_logger = logger;
	}

	/// <summary>
	/// Handles the creation of a new article.
	/// </summary>
	/// <param name="article">The article to create.</param>
	/// <param name="cancellationToken">The cancellation token.</param>
	/// <returns>A result containing the created article or an error message.</returns>
	public async Task<Result<Article>> HandleAsync(Article article, CancellationToken cancellationToken = default)
	{
		if (article is null)
		{
			_logger.LogWarning("CreateArticle: Article cannot be null");
			return Result<Article>.Failure("Article cannot be null");
		}

		// Validate the article
		var validationResult = await _validator.ValidateAsync(article, cancellationToken);
		if (!validationResult.IsValid)
		{
			var errors = string.Join(", ", validationResult.Errors.Select(e => e.ErrorMessage));
			_logger.LogWarning("CreateArticle: Validation failed. Errors: {Errors}", errors);
			return Result<Article>.Failure(errors);
		}

		// Set creation timestamp
		article.CreatedAt = DateTime.UtcNow;
		article.UpdatedAt = DateTime.UtcNow;

		// Create the article in the repository
		var result = await _repository.CreateAsync(article, cancellationToken);

		if (result.IsSuccess)
		{
			_logger.LogInformation("CreateArticle: Article created successfully with ID: {ArticleId}", article.Id);
		}
		else
		{
			_logger.LogError("CreateArticle: Failed to create article. Error: {Error}", result.Error);
		}

		return result;
	}
}
