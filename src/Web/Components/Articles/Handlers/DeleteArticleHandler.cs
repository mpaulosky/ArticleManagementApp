// =======================================================
// Copyright (c) 2025. All rights reserved.
// File Name :     DeleteArticleHandler.cs
// Company :       ArticleManagementApp
// Author :        GitHub Copilot
// Solution Name : ArticleManagementApp
// Project Name :  Web
// =======================================================

namespace Web.Components.Articles.Handlers;

/// <summary>
/// Handler for deleting an article.
/// </summary>
public sealed class DeleteArticleHandler
{
	private readonly IArticleRepository _repository;
	private readonly ILogger<DeleteArticleHandler> _logger;

	/// <summary>
	/// Initializes a new instance of the <see cref="DeleteArticleHandler"/> class.
	/// </summary>
	public DeleteArticleHandler(
		IArticleRepository repository,
		ILogger<DeleteArticleHandler> logger)
	{
		_repository = repository;
		_logger = logger;
	}

	/// <summary>
	/// Handles the deletion of an article.
	/// </summary>
	/// <param name="articleId">The ID of the article to delete.</param>
	/// <param name="cancellationToken">The cancellation token.</param>
	/// <returns>A result indicating success or failure.</returns>
	public async Task<Result> HandleAsync(string articleId, CancellationToken cancellationToken = default)
	{
		if (string.IsNullOrWhiteSpace(articleId))
		{
			_logger.LogWarning("DeleteArticle: Article ID cannot be null or empty");
			return Result.Failure("Article ID is required");
		}

		// Delete the article
		var result = await _repository.DeleteAsync(articleId, cancellationToken);

		if (result.IsSuccess)
		{
			_logger.LogInformation("DeleteArticle: Article {ArticleId} deleted successfully", articleId);
		}
		else
		{
			_logger.LogError("DeleteArticle: Failed to delete article {ArticleId}. Error: {Error}", 
				articleId, result.Error);
		}

		return result;
	}
}
