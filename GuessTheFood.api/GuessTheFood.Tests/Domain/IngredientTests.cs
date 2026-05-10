using FluentAssertions;
using GuessTheFood.api.Domain.Entities;

namespace GuessTheFood.Tests.Domain;

public class IngredientTests
{
    [Fact]
    public void Constructor_Should_CreateIngredient_WhenNameIsValid()
    {
        var ingredient = new Ingredient("Tomate");

        ingredient.Id.Should().NotBeEmpty();
        ingredient.Name.Should().Be("tomate");
    }

    [Fact]
    public void Constructor_Should_TrimName()
    {
        var ingredient = new Ingredient("  Tomate  ");

        ingredient.Name.Should().Be("tomate");
    }

    [Fact]
    public void Constructor_Should_Throw_WhenNameIsEmpty()
    {
        var act = () => new Ingredient("");

        act.Should().Throw<ArgumentException>();
    }

    [Fact]
    public void UpdateName_Should_UpdateName_WhenNameIsValid()
    {
        var ingredient = new Ingredient("Tomate");

        ingredient.UpdateName("Sel");

        ingredient.Name.Should().Be("sel");
    }
}