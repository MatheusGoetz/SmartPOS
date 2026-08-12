using MediatR;

namespace SmartPOS.Application.Features.Categories.CreateCategory;

public sealed record CreateCategoryCommand(
    string Name,
    string? Description
) : IRequest<CreateCategoryResponse>;