using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SmartPOS.Application.Common.Interface;
using SmartPOS.Application.Common.Interface.Repositories;
using SmartPOS.Infrastructure.Persistence;
using SmartPOS.Infrastructure.Persistence.Repositories;

namespace SmartPOS.Infrastructure.DependencyInjection;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddDbContext<SmartPosDbContext>(options =>
            options.UseNpgsql(
                configuration.GetConnectionString("DefaultConnection")));

        services.AddScoped<ICategoryRepository, CategoryRepository>();

        services.AddScoped<IUnitOfWork>(provider =>
        provider.GetRequiredService<SmartPosDbContext>());

        return services;
    }
}