using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace ArticleManagementApp.Shared.Entities;

/// <summary>
/// Represents a blog article with content, metadata, and publication status.
/// </summary>
public sealed class Article
{
    /// <summary>
    /// Gets or sets the unique identifier for the article.
    /// </summary>
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the article title.
    /// </summary>
    [BsonElement("title")]
    [BsonRequired]
    public string Title { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the URL-friendly slug for the article.
    /// </summary>
    [BsonElement("slug")]
    [BsonRequired]
    public string Slug { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the article summary/excerpt.
    /// </summary>
    [BsonElement("summary")]
    public string Summary { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the markdown content of the article.
    /// </summary>
    [BsonElement("content")]
    [BsonRequired]
    public string Content { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the author name.
    /// </summary>
    [BsonElement("author")]
    [BsonRequired]
    public string Author { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the category ID this article belongs to.
    /// </summary>
    [BsonElement("categoryId")]
    [BsonRepresentation(BsonType.ObjectId)]
    public string CategoryId { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the list of tags associated with the article.
    /// </summary>
    [BsonElement("tags")]
    public List<string> Tags { get; set; } = [];

    /// <summary>
    /// Gets or sets whether the article is published.
    /// </summary>
    [BsonElement("isPublished")]
    public bool IsPublished { get; set; }

    /// <summary>
    /// Gets or sets the date and time when the article was created.
    /// </summary>
    [BsonElement("createdAt")]
    [BsonDateTimeOptions(Kind = DateTimeKind.Utc)]
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    /// <summary>
    /// Gets or sets the date and time when the article was last updated.
    /// </summary>
    [BsonElement("updatedAt")]
    [BsonDateTimeOptions(Kind = DateTimeKind.Utc)]
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

    /// <summary>
    /// Gets or sets the date and time when the article was published.
    /// </summary>
    [BsonElement("publishedAt")]
    [BsonDateTimeOptions(Kind = DateTimeKind.Utc)]
    public DateTime? PublishedAt { get; set; }

    /// <summary>
    /// Gets or sets the view count for the article.
    /// </summary>
    [BsonElement("viewCount")]
    public int ViewCount { get; set; }
}
