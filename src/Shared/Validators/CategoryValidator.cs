using ArticleManagementApp.Shared.Entities;
using FluentValidation;

namespace ArticleManagementApp.Shared.Validators;

/// <summary>
/// Validator for Category entity.
/// </summary>
public sealed class CategoryValidator : AbstractValidator<Category>
{
    public CategoryValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Category name is required.")
            .MaximumLength(100).WithMessage("Category name must not exceed 100 characters.");

        RuleFor(x => x.Slug)
            .NotEmpty().WithMessage("Slug is required.")
            .MaximumLength(150).WithMessage("Slug must not exceed 150 characters.")
            .Matches(@"^[a-z0-9]+(?:-[a-z0-9]+)*$")
            .WithMessage("Slug must be lowercase alphanumeric with hyphens only.");

        RuleFor(x => x.Description)
            .MaximumLength(500).WithMessage("Description must not exceed 500 characters.");

        RuleFor(x => x.ParentId)
            .Matches(@"^[0-9a-fA-F]{24}$")
            .When(x => !string.IsNullOrWhiteSpace(x.ParentId))
            .WithMessage("Parent category ID must be a valid MongoDB ObjectId.");

        RuleFor(x => x.DisplayOrder)
            .GreaterThanOrEqualTo(0)
            .WithMessage("Display order cannot be negative.");
    }
}
