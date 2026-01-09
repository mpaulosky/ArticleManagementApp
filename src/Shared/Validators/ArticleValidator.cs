using ArticleManagementApp.Shared.Entities;
using FluentValidation;

namespace ArticleManagementApp.Shared.Validators;

/// <summary>
/// Validator for Article entity.
/// </summary>
public sealed class ArticleValidator : AbstractValidator<Article>
{
	public ArticleValidator()
	{
		RuleFor(x => x.Title)
				.NotEmpty().WithMessage("Title is required.")
				.MaximumLength(200).WithMessage("Title must not exceed 200 characters.");

		RuleFor(x => x.Slug)
				.NotEmpty().WithMessage("Slug is required.")
				.MaximumLength(250).WithMessage("Slug must not exceed 250 characters.")
				.Matches(@"^[a-z0-9]+(?:-[a-z0-9]+)*$")
				.WithMessage("Slug must be lowercase alphanumeric with hyphens only.");

		RuleFor(x => x.Content)
				.NotEmpty().WithMessage("Content is required.");

		RuleFor(x => x.Author)
				.NotEmpty().WithMessage("Author is required.")
				.MaximumLength(100).WithMessage("Author name must not exceed 100 characters.");

		RuleFor(x => x.Summary)
				.MaximumLength(500).WithMessage("Summary must not exceed 500 characters.");

		RuleFor(x => x.CategoryId)
				.NotEmpty().WithMessage("Category is required.")
				.Matches(@"^[0-9a-fA-F]{24}$")
				.WithMessage("Category ID must be a valid MongoDB ObjectId.");

		RuleFor(x => x.Tags)
				.Must(tags => tags == null || tags.Count <= 10)
				.WithMessage("Article cannot have more than 10 tags.");

		RuleFor(x => x.ViewCount)
				.GreaterThanOrEqualTo(0)
				.WithMessage("View count cannot be negative.");

		RuleFor(x => x.PublishedAt)
				.LessThanOrEqualTo(DateTime.UtcNow)
				.When(x => x.PublishedAt.HasValue)
				.WithMessage("Published date cannot be in the future.");
	}
}
