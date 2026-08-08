using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SmartPOS.Domain.Entities;

namespace SmartPOS.Infrastructure.Persistence.Configurations
{
    public sealed class CategoryConfiguration : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder.ToTable("Categories");
            builder.HasKey(c => c.Id);
            builder.Property(c => c.Name)
            .IsRequired()
            .HasMaxLength(100);
            builder.Property(c => c.Description)
            .HasMaxLength(255);
            builder.HasIndex(c => c.Name);
        }
    }
}