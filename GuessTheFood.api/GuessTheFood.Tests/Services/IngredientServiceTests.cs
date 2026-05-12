using FluentAssertions;
using GuessTheFood.api.Application.DTOs.Ingredients;
using GuessTheFood.api.Application.Services;
using GuessTheFood.api.Domain.Enums;
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
    public async Task CreateAsync_Should_CreateIngredient_WhenNameAndTypeAreValid()
    {
        await using var context = CreateDbContext();
        var service = new IngredientService(context);

        var result = await service.CreateAsync(new CreateIngredientDto
        {
            Name = "Tomate",
            Type = IngredientType.Vegetable
        });

        result.Id.Should().NotBeEmpty();
        result.Name.Should().Be("Tomate");
        result.Type.Should().Be(IngredientType.Vegetable);

        context.Ingredients.Should().ContainSingle();
    }

    [Fact]
    public async Task CreateAsync_Should_Throw_WhenIngredientAlreadyExists()
    {
        await using var context = CreateDbContext();
        var service = new IngredientService(context);

        await service.CreateAsync(new CreateIngredientDto
        {
            Name = "Tomate",
            Type = IngredientType.Vegetable
        });

        var act = async () => await service.CreateAsync(new CreateIngredientDto
        {
            Name = " Tomate ",
            Type = IngredientType.Vegetable
        });

        await act.Should().ThrowAsync<InvalidOperationException>();
    }

    [Fact]
    public async Task GetAllAsync_Should_ReturnAllIngredients()
    {
        await using var context = CreateDbContext();
        var service = new IngredientService(context);

        await service.CreateAsync(new CreateIngredientDto
        {
            Name = "Tomate",
            Type = IngredientType.Vegetable
        });

        await service.CreateAsync(new CreateIngredientDto
        {
            Name = "Sel",
            Type = IngredientType.Spice
        });

        var result = await service.GetAllAsync();

        result.Should().HaveCount(2);
        result.Select(i => i.Name).Should().Contain(["Tomate", "Sel"]);
        result.Select(i => i.Type).Should().Contain([IngredientType.Vegetable, IngredientType.Spice]);
    }

    [Fact]
    public async Task GetByIdAsync_Should_ReturnIngredient_WhenExists()
    {
        await using var context = CreateDbContext();
        var service = new IngredientService(context);

        var created = await service.CreateAsync(new CreateIngredientDto
        {
            Name = "Tomate",
            Type = IngredientType.Vegetable
        });

        var result = await service.GetByIdAsync(created.Id);

        result.Should().NotBeNull();
        result!.Id.Should().Be(created.Id);
        result.Name.Should().Be("Tomate");
        result.Type.Should().Be(IngredientType.Vegetable);
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
    public async Task GetByTypeAsync_Should_ReturnIngredients_WhenTypeExists()
    {
        await using var context = CreateDbContext();
        var service = new IngredientService(context);

        await service.CreateAsync(new CreateIngredientDto
        {
            Name = "Tomate",
            Type = IngredientType.Vegetable
        });

        await service.CreateAsync(new CreateIngredientDto
        {
            Name = "Sel",
            Type = IngredientType.Spice
        });

        var result = await service.GetByTypeAsync(IngredientType.Vegetable);

        result.Should().ContainSingle();
        result[0].Name.Should().Be("Tomate");
        result[0].Type.Should().Be(IngredientType.Vegetable);
    }

    [Fact]
    public async Task GetByNameAsync_Should_ReturnIngredient_WhenNameExists()
    {
        await using var context = CreateDbContext();
        var service = new IngredientService(context);

        await service.CreateAsync(new CreateIngredientDto
        {
            Name = "Tomate",
            Type = IngredientType.Vegetable
        });

        var result = await service.GetByNameAsync(" Tomate ");

        result.Should().NotBeNull();
        result!.Name.Should().Be("Tomate");
        result.Type.Should().Be(IngredientType.Vegetable);
    }

    [Fact]
    public async Task GetByNameAsync_Should_ReturnNull_WhenNameDoesNotExist()
    {
        await using var context = CreateDbContext();
        var service = new IngredientService(context);

        var result = await service.GetByNameAsync("Inconnu");

        result.Should().BeNull();
    }

    [Fact]
    public async Task UpdateAsync_Should_UpdateIngredient_WhenExists()
    {
        await using var context = CreateDbContext();
        var service = new IngredientService(context);

        var created = await service.CreateAsync(new CreateIngredientDto
        {
            Name = "Tomate",
            Type = IngredientType.Vegetable
        });

        var updated = await service.UpdateAsync(created.Id, new UpdateIngredientDto
        {
            Name = "Sel",
            Type = IngredientType.Spice
        });

        updated.Should().BeTrue();

        var result = await service.GetByIdAsync(created.Id);
        result!.Name.Should().Be("Sel");
        result.Type.Should().Be(IngredientType.Spice);
    }

    [Fact]
    public async Task UpdateAsync_Should_ReturnFalse_WhenIngredientDoesNotExist()
    {
        await using var context = CreateDbContext();
        var service = new IngredientService(context);

        var result = await service.UpdateAsync(Guid.NewGuid(), new UpdateIngredientDto
        {
            Name = "Sel",
            Type = IngredientType.Spice
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
            Name = "Tomate",
            Type = IngredientType.Vegetable
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