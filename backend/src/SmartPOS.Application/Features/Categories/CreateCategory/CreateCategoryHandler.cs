using MediatR;
using SmartPOS.Application.Common.Exceptions;
using SmartPOS.Application.Common.Interface;
using SmartPOS.Application.Common.Interface.Repositories;
using SmartPOS.Domain.Entities;

namespace SmartPOS.Application.Features.Categories.CreateCategory
{
    public sealed class CreateCategoryHandler : IRequestHandler<CreateCategoryCommand, CreateCategoryResponse>
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IUnitOfWork _unitOfWork;

        public CreateCategoryHandler(ICategoryRepository categoryRepository,
        IUnitOfWork unitOfWork)
        {
            _categoryRepository = categoryRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<CreateCategoryResponse> Handle(
            CreateCategoryCommand request,
            CancellationToken cancellationToken)
        {
            var categoryName = request.Name.Trim();

            var categoryAlreadyExists =
                await _categoryRepository.ExistsByNameAsync(
                    categoryName,
                    cancellationToken);

            if (categoryAlreadyExists)
            {
                throw new ConflictException(
                    $"A category named '{request.Name}' already exists."
                );
            }

            var category = new Category(
                categoryName,
                request.Description?.Trim());

            await _categoryRepository.AddAsync(
                category,
                cancellationToken
            );

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return new CreateCategoryResponse(
                category.Id,
                category.Name,
                category.Description,
                category.CreatedAt
            );
        }
    }
}