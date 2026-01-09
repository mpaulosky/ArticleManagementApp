// =======================================================
// Copyright (c) 2025. All rights reserved.
// File Name :     SearchArticlesHandler.cs
// Company :       ArticleManagementApp
// Author :        GitHub Copilot
// Solution Name : ArticleManagementApp
// Project Name :  Web
// =======================================================

namespace Web.Components.Articles.Handlers;

/// <summary>
/// Handler for searching articles by query string.
/// </summary>
public sealed class SearchArticlesHandler
{
	private readonly IArticleRepository _repository;
	private readonly ILogger<SearchArticlesHandler> _logger;

	/// <summary>
	/// Initializes a new instance of the <see cref="SearchArticlesHandler"/> class.
	/// </summary>
	public SearchArticlesHandler(
		IArticleRepository repository,
		ILogger<SearchArticlesHandler> logger)
	{
		_repository = repository;
		_logger = logger;
	}

	/// <summary>
	/// Handles the search of articles by query string.
	/// Searches across title, content, and summary fields.
	/// </summary>
	/// <param name="query">The search query string.</param>
	/// <param name="cancellationToken">The cancellation token.</param>
	/// <returns>A result containing the list of matching articles or an error message.</returns>
	public async Task<Result<List<Article>>> HandleAsync(string query, CancellationToken cancellationToken = default)
	{
		if (string.IsNullOrWhiteSpace(query))
		{
			_logger.LogWarning("SearchArticles: Query cannot be null or empty");
			return Result<List<Article>>.Failure("Search query is required");
		}

		try
		{
			var articles = await _repository.SearchAsync(query, cancellationToken);
			_logger.LogInformation("SearchArticles: Found {Count} articles matching query '{Query}'",
				articles.Count, query);
			return Result<List<Article>>.Success(articles);
		}
		catch (Exception ex)
		{
			_logger.LogError(ex, "SearchArticles: Error searching articles with query '{Query}'", query);
			return Result<List<Article>>.Failure($"Error searching articles: {ex.Message}");
		}
	}
}
