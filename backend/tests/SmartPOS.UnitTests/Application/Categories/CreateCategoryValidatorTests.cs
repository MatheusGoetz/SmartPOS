using SmartPOS.Application.Features.Categories.CreateCategory;

namespace SmartPOS.UnitTests.Application.Categories;

public sealed class CreateCategoryValidatorTests
{
    private readonly CreateCategoryValidator _validator = new();

    [Fact]
    public async Task Should_Pass_When_Category_Is_Valid()
    {
        var command = new CreateCategoryCommand(
            "Bebidas",
            "Bebidas, refrigerantes e sucos");

        var result = await _validator.ValidateAsync(command);

        Assert.True(result.IsValid);
    }

    [Fact]
    public async Task Should_Fail_When_Name_Is_Empty()
    {
        var command = new CreateCategoryCommand(
            string.Empty,
            "Description");

        var result = await _validator.ValidateAsync(command);

        Assert.False(result.IsValid);

        Assert.Contains(
            result.Errors,
            error => error.PropertyName == nameof(command.Name));
    }

    [Fact]
    public async Task Should_Fail_When_Name_Exceeds_Maximum_Length()
    {
        var command = new CreateCategoryCommand(
            new string('A', 101),
            null);

        var result = await _validator.ValidateAsync(command);

        Assert.False(result.IsValid);
    }
}