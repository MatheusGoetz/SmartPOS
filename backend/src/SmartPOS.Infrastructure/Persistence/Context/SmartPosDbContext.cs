using Microsoft.EntityFrameworkCore;
using SmartPOS.Application.Common.Interface;
using SmartPOS.Domain.Entities;

namespace SmartPOS.Infrastructure.Persistence
{
    public sealed class SmartPosDbContext : DbContext, IUnitOfWork
    {
        public SmartPosDbContext(DbContextOptions<SmartPosDbContext> options) : base(options)
        {
        }

        public DbSet<Category> Categories => Set<Category>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfigurationsFromAssembly(typeof(SmartPosDbContext).Assembly);
        }
    }
}