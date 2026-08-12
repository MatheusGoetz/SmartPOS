namespace SmartPOS.Application.Features.Categories.CreateCategory;

public sealed record CreateCategoryResponse(
    Guid Id,
    string Name,
    string? Description,
    DateTime CreatedAt
);