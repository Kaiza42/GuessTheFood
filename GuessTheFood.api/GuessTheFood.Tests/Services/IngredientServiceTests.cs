using FluentAssertions;
using GuessTheFood.api.Application.DTOs.Ingredients;
using GuessTheFood.api.Application.Services;
using GuessTheFood.Api.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace GuessTheFood.Tests.Application.Services;

public class IngredientServiceTests
{
    private static AppDbContext CreateDbContext()
    {
        var options = new DbContextOptionsBuilder<AppDbContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString())
            .Options;

        return new AppDbContext(options);
    }

    [Fact]
    public async Task CreateAsync_Should_CreateIngredient_WhenNameIsValid()
    {
        await using var context = CreateDbContext();
        var service = new IngredientService(context);

        var result = await service.CreateAsync(new CreateIngredientDto
        {
            Name = "Tomate"
        });

        result.Id.Should().NotBeEmpty();
        result.Name.Should().Be("tomate");

        context.Ingredients.Should().ContainSingle();
    }

    [Fact]
    public async Task CreateAsync_Should_Throw_WhenIngredientAlreadyExists()
    {
        await using var context = CreateDbContext();
        var service = new IngredientService(context);

        await service.CreateAsync(new CreateIngredientDto
        {
            Name = "Tomate"
        });

        var act = async () => await service.CreateAsync(new CreateIngredientDto
        {
            Name = " tomate "
        });

        await act.Should().ThrowAsync<InvalidOperationException>();
    }

    [Fact]
    public async Task GetAllAsync_Should_ReturnAllIngredients()
    {
        await using var context = CreateDbContext();
        var service = new IngredientService(context);

        await service.CreateAsync(new CreateIngredientDto { Name = "Tomate" });
        await service.CreateAsync(new CreateIngredientDto { Name = "Sel" });

        var result = await service.GetAllAsync();

        result.Should().HaveCount(2);
        result.Select(i => i.Name).Should().Contain(["tomate", "sel"]);
    }

    [Fact]
    public async Task GetByIdAsync_Should_ReturnIngredient_WhenExists()
    {
        await using var context = CreateDbContext();
        var service = new IngredientService(context);

        var created = await service.CreateAsync(new CreateIngredientDto
        {
            Name = "Tomate"
        });

        var result = await service.GetByIdAsync(created.Id);

        result.Should().NotBeNull();
        result!.Id.Should().Be(created.Id);
        result.Name.Should().Be("tomate");
    }

    [Fact]
    public async Task GetByIdAsync_Should_ReturnNull_WhenIngredientDoesNotExist()
    {
        await using var context = CreateDbContext();
        var service = new IngredientService(context);

        var result = await service.GetByIdAsync(Guid.NewGuid());

        result.Should().BeNull();
    }

    [Fact]
    public async Task UpdateAsync_Should_UpdateIngredient_WhenExists()
    {
        await using var context = CreateDbContext();
        var service = new IngredientService(context);

        var created = await service.CreateAsync(new CreateIngredientDto
        {
            Name = "Tomate"
        });

        var updated = await service.UpdateAsync(created.Id, new UpdateIngredientDto
        {
            Name = "Sel"
        });

        updated.Should().BeTrue();

        var result = await service.GetByIdAsync(created.Id);
        result!.Name.Should().Be("sel");
    }

    [Fact]
    public async Task UpdateAsync_Should_ReturnFalse_WhenIngredientDoesNotExist()
    {
        await using var context = CreateDbContext();
        var service = new IngredientService(context);

        var result = await service.UpdateAsync(Guid.NewGuid(), new UpdateIngredientDto
        {
            Name = "Sel"
        });

        result.Should().BeFalse();
    }

    [Fact]
    public async Task DeleteAsync_Should_DeleteIngredient_WhenExists()
    {
        await using var context = CreateDbContext();
        var service = new IngredientService(context);

        var created = await service.CreateAsync(new CreateIngredientDto
        {
            Name = "Tomate"
        });

        var deleted = await service.DeleteAsync(created.Id);

        deleted.Should().BeTrue();

        var result = await service.GetByIdAsync(created.Id);
        result.Should().BeNull();
    }

    [Fact]
    public async Task DeleteAsync_Should_ReturnFalse_WhenIngredientDoesNotExist()
    {
        await using var context = CreateDbContext();
        var service = new IngredientService(context);

        var result = await service.DeleteAsync(Guid.NewGuid());

        result.Should().BeFalse();
    }
}