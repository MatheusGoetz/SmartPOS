using System.Net;
using System.Net.Http.Json;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using SmartPOS.Application.Features.Categories.CreateCategory;
using SmartPOS.Infrastructure.Persistence;
using SmartPOS.IntegrationTests.Infrastructure;
using Xunit;

namespace SmartPOS.IntegrationTests.Categories;

public sealed class CreateCategoryTests
    : IClassFixture<CustomWebApplicationFactory>
{
    private readonly CustomWebApplicationFactory _factory;

    public CreateCategoryTests(
        CustomWebApplicationFactory factory)
    {
        _factory = factory;
    }

    [Fact]
    public async Task Should_Create_Category_When_Request_Is_Valid()
    {
        await PrepareDatabaseAsync();

        using var client = _factory.CreateClient();

        var command = new CreateCategoryCommand(
            "Bebidas",
            "Bebidas, refrigerantes e sucos");

        var response = await client.PostAsJsonAsync(
            "/api/v1/categories",
            command);

        Assert.Equal(
            HttpStatusCode.Created,
            response.StatusCode);

        var content =
            await response.Content
                .ReadFromJsonAsync<CreateCategoryResponse>();

        Assert.NotNull(content);
        Assert.Equal("Bebidas", content.Name);

        await using var scope =
            _factory.Services.CreateAsyncScope();

        var dbContext =
            scope.ServiceProvider
                .GetRequiredService<SmartPosDbContext>();

        var categoryExists =
            await dbContext.Categories
                .AnyAsync(category =>
                    category.Id == content.Id &&
                    category.Name == "Bebidas");

        Assert.True(categoryExists);
    }

    [Fact]
    public async Task Should_Return_BadRequest_When_Name_Is_Empty()
    {
        await PrepareDatabaseAsync();

        using var client = _factory.CreateClient();

        var command = new CreateCategoryCommand(
            string.Empty,
            "Teste");

        var response = await client.PostAsJsonAsync(
            "/api/v1/categories",
            command);

        Assert.Equal(
            HttpStatusCode.BadRequest,
            response.StatusCode);
    }

    [Fact]
    public async Task Should_Return_Conflict_When_Category_Already_Exists()
    {
        await PrepareDatabaseAsync();

        using var client = _factory.CreateClient();

        var firstCommand = new CreateCategoryCommand(
            "Bebidas",
            "Primeira categoria");

        var secondCommand = new CreateCategoryCommand(
            "Bebidas",
            "Categoria duplicada");

        var firstResponse =
            await client.PostAsJsonAsync(
                "/api/v1/categories",
                firstCommand);

        var secondResponse =
            await client.PostAsJsonAsync(
                "/api/v1/categories",
                secondCommand);

        Assert.Equal(
            HttpStatusCode.Created,
            firstResponse.StatusCode);

        Assert.Equal(
            HttpStatusCode.Conflict,
            secondResponse.StatusCode);
    }

    private async Task PrepareDatabaseAsync()
{
    await using var scope =
        _factory.Services.CreateAsyncScope();

    var dbContext =
        scope.ServiceProvider
            .GetRequiredService<SmartPosDbContext>();

    var databaseName =
        dbContext.Database.GetDbConnection().Database;

    if (!databaseName.EndsWith(
            "_test",
            StringComparison.OrdinalIgnoreCase))
    {
        throw new InvalidOperationException(
            $"Integration tests cannot run against database '{databaseName}'.");
    }

    await dbContext.Database.MigrateAsync();

    await dbContext.Database.ExecuteSqlRawAsync(
        """TRUNCATE TABLE "Categories";""");
}
}