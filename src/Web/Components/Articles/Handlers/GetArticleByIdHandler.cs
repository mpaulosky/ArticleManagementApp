// =======================================================
// Copyright (c) 2025. All rights reserved.
// File Name :     GetArticleByIdHandler.cs
// Company :       ArticleManagementApp
// Author :        GitHub Copilot
// Solution Name : ArticleManagementApp
// Project Name :  Web
// =======================================================

namespace Web.Components.Articles.Handlers;

/// <summary>
/// Handler for retrieving a single article by its ID.
/// </summary>
public sealed class GetArticleByIdHandler
{
	private readonly IArticleRepository _repository;
	private readonly ILogger<GetArticleByIdHandler> _logger;

	/// <summary>
	/// Initializes a new instance of the <see cref="GetArticleByIdHandler"/> class.
	/// </summary>
	public GetArticleByIdHandler(
		IArticleRepository repository,
		ILogger<GetArticleByIdHandler> logger)
	{
		_repository = repository;
		_logger = logger;
	}

	/// <summary>
	/// Handles the retrieval of an article by its ID.
	/// </summary>
	/// <param name="articleId">The ID of the article to retrieve.</param>
	/// <param name="cancellationToken">The cancellation token.</param>
	/// <returns>A result containing the article or an error message.</returns>
	public async Task<Result<Article?>> HandleAsync(string articleId, CancellationToken cancellationToken = default)
	{
		if (string.IsNullOrWhiteSpace(articleId))
		{
			_logger.LogWarning("GetArticleById: Article ID cannot be null or empty");
			return Result<Article?>.Failure("Article ID is required");
		}

		try
		{
			var article = await _repository.GetByIdAsync(articleId, cancellationToken);
			if (article is null)
			{
				_logger.LogInformation("GetArticleById: Article {ArticleId} not found", articleId);
				return Result<Article?>.Failure($"Article with ID {articleId} not found");
			}

			_logger.LogInformation("GetArticleById: Article {ArticleId} retrieved successfully", articleId);
			return Result<Article?>.Success(article);
		}
		catch (Exception ex)
		{
			_logger.LogError(ex, "GetArticleById: Error retrieving article {ArticleId}", articleId);
			return Result<Article?>.Failure($"Error retrieving article: {ex.Message}");
		}
	}
}
