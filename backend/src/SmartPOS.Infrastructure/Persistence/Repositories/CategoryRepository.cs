using Microsoft.EntityFrameworkCore;
using SmartPOS.Application.Common.Interface.Repositories;
using SmartPOS.Domain.Entities;

namespace SmartPOS.Infrastructure.Persistence.Repositories
{
    public sealed class CategoryRepository : ICategoryRepository
    {
        private readonly SmartPosDbContext _dbContext;

        public CategoryRepository(SmartPosDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task AddAsync(
            Category category,
            CancellationToken cancellationToken = default)
        {
            await _dbContext.Categories.AddAsync(
            category,
            cancellationToken);
        }

        public async Task<Category?> GetByIdAsync(
            Guid id,
            CancellationToken cancellationToken = default)
        {
            return await _dbContext.Categories
                .FirstOrDefaultAsync(
                category => category.Id == id,
                cancellationToken);
        }

        public async Task<bool> ExistsByNameAsync(
            string name,
            CancellationToken cancellationToken = default)
        {
            return await _dbContext.Categories
                .AnyAsync(
                    category => category.Name == name,
                    cancellationToken
                );
        }
    }
}