// =======================================================
// Copyright (c) 2025. All rights reserved.
// File Name :     UpdateArticleHandler.cs
// Company :       ArticleManagementApp
// Author :        GitHub Copilot
// Solution Name : ArticleManagementApp
// Project Name :  Web
// =======================================================

namespace Web.Components.Articles.Handlers;

/// <summary>
/// Handler for updating an existing article with validation and persistence.
/// </summary>
public sealed class UpdateArticleHandler
{
	private readonly IArticleRepository _repository;
	private readonly IValidator<Article> _validator;
	private readonly ILogger<UpdateArticleHandler> _logger;

	/// <summary>
	/// Initializes a new instance of the <see cref="UpdateArticleHandler"/> class.
	/// </summary>
	public UpdateArticleHandler(
		IArticleRepository repository,
		IValidator<Article> validator,
		ILogger<UpdateArticleHandler> logger)
	{
		_repository = repository;
		_validator = validator;
		_logger = logger;
	}

	/// <summary>
	/// Handles the update of an existing article.
	/// </summary>
	/// <param name="article">The article with updated values.</param>
	/// <param name="cancellationToken">The cancellation token.</param>
	/// <returns>A result containing the updated article or an error message.</returns>
	public async Task<Result<Article>> HandleAsync(Article article, CancellationToken cancellationToken = default)
	{
		if (article is null)
		{
			_logger.LogWarning("UpdateArticle: Article cannot be null");
			return Result<Article>.Failure("Article cannot be null");
		}

		if (string.IsNullOrEmpty(article.Id))
		{
			_logger.LogWarning("UpdateArticle: Article ID cannot be null or empty");
			return Result<Article>.Failure("Article ID is required");
		}

		// Validate the article
		var validationResult = await _validator.ValidateAsync(article, cancellationToken);
		if (!validationResult.IsValid)
		{
			var errors = string.Join(", ", validationResult.Errors.Select(e => e.ErrorMessage));
			_logger.LogWarning("UpdateArticle: Validation failed for article {ArticleId}. Errors: {Errors}", 
				article.Id, errors);
			return Result<Article>.Failure(errors);
		}

		// Update the modified timestamp
		article.UpdatedAt = DateTime.UtcNow;

		// Update the article in the repository
		var result = await _repository.UpdateAsync(article, cancellationToken);

		if (result.IsSuccess)
		{
			_logger.LogInformation("UpdateArticle: Article {ArticleId} updated successfully", article.Id);
		}
		else
		{
			_logger.LogError("UpdateArticle: Failed to update article {ArticleId}. Error: {Error}", 
				article.Id, result.Error);
		}

		return result;
	}
}
