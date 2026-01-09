using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace ArticleManagementApp.Shared.Entities;

/// <summary>
/// Represents a category for organizing articles.
/// </summary>
public sealed class Category
{
    /// <summary>
    /// Gets or sets the unique identifier for the category.
    /// </summary>
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the category name.
    /// </summary>
    [BsonElement("name")]
    [BsonRequired]
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the URL-friendly slug for the category.
    /// </summary>
    [BsonElement("slug")]
    [BsonRequired]
    public string Slug { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the category description.
    /// </summary>
    [BsonElement("description")]
    public string Description { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the parent category ID for hierarchical categories.
    /// </summary>
    [BsonElement("parentId")]
    [BsonRepresentation(BsonType.ObjectId)]
    public string? ParentId { get; set; }

    /// <summary>
    /// Gets or sets the date and time when the category was created.
    /// </summary>
    [BsonElement("createdAt")]
    [BsonDateTimeOptions(Kind = DateTimeKind.Utc)]
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    /// <summary>
    /// Gets or sets the date and time when the category was last updated.
    /// </summary>
    [BsonElement("updatedAt")]
    [BsonDateTimeOptions(Kind = DateTimeKind.Utc)]
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

    /// <summary>
    /// Gets or sets the display order for the category.
    /// </summary>
    [BsonElement("displayOrder")]
    public int DisplayOrder { get; set; }

    /// <summary>
    /// Gets or sets whether the category is active.
    /// </summary>
    [BsonElement("isActive")]
    public bool IsActive { get; set; } = true;
}
