using FluentValidation;

namespace SmartPOS.Application.Features.Categories.CreateCategory
{
    public sealed class CreateCategoryValidator : AbstractValidator<CreateCategoryCommand>
    {
        public CreateCategoryValidator()
        {
            RuleFor(category => category.Name)
            .NotEmpty()
            .WithMessage("Category name is required").MaximumLength(100)
            .WithMessage("Category name must contain at most 100 characters.");

            RuleFor(category => category.Description)
            .MaximumLength(255)
            .WithMessage("Category description must contain at most 255 characters.");
        }
    }
}