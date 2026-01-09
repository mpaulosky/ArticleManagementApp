// =======================================================
// Copyright (c) 2025. All rights reserved.
// File Name :     GetAllArticlesHandler.cs
// Company :       ArticleManagementApp
// Author :        GitHub Copilot
// Solution Name : ArticleManagementApp
// Project Name :  Web
// =======================================================

namespace Web.Components.Articles.Handlers;

/// <summary>
/// Handler for retrieving all articles with optional filtering.
/// </summary>
public sealed class GetAllArticlesHandler
{
	private readonly IArticleRepository _repository;
	private readonly ILogger<GetAllArticlesHandler> _logger;

	/// <summary>
	/// Initializes a new instance of the <see cref="GetAllArticlesHandler"/> class.
	/// </summary>
	public GetAllArticlesHandler(
		IArticleRepository repository,
		ILogger<GetAllArticlesHandler> logger)
	{
		_repository = repository;
		_logger = logger;
	}

	/// <summary>
	/// Handles the retrieval of all articles.
	/// </summary>
	/// <param name="isPublishedOnly">If true, only published articles are returned.</param>
	/// <param name="categoryId">Optional category ID to filter articles by category.</param>
	/// <param name="cancellationToken">The cancellation token.</param>
	/// <returns>A result containing the list of articles or an error message.</returns>
	public async Task<Result<List<Article>>> HandleAsync(
		bool isPublishedOnly = false,
		string? categoryId = null,
		CancellationToken cancellationToken = default)
	{
		try
		{
			var articles = await _repository.GetAllAsync(isPublishedOnly, categoryId, cancellationToken);
			_logger.LogInformation("GetAllArticles: Retrieved {Count} articles (published only: {PublishedOnly}, category: {CategoryId})",
				articles.Count, isPublishedOnly, categoryId ?? "none");
			return Result<List<Article>>.Success(articles);
		}
		catch (Exception ex)
		{
			_logger.LogError(ex, "GetAllArticles: Error retrieving articles");
			return Result<List<Article>>.Failure($"Error retrieving articles: {ex.Message}");
		}
	}
}
