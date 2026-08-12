using SmartPOS.Domain.Entities;

namespace SmartPOS.Application.Common.Interface.Repositories
{
    public interface ICategoryRepository
    {
        Task AddAsync(
            Category category, CancellationToken cancellationToken = default);

        Task<Category?> GetByIdAsync(
            Guid id,
            CancellationToken cancellationToken = default);

        Task<bool> ExistsByNameAsync(
        string name,
        CancellationToken cancellationToken = default);
    }
}