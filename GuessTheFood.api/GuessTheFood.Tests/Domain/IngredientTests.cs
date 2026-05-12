using FluentAssertions;
using GuessTheFood.api.Domain.Entities;
using GuessTheFood.api.Domain.Enums;

namespace GuessTheFood.Tests.Domain;

public class IngredientTests
{
    [Fact]
    public void Constructor_Should_CreateIngredient_WhenNameAndTypeAreValid()
    {
        var ingredient = new Ingredient("Tomate", IngredientType.Vegetable);

        ingredient.Id.Should().NotBeEmpty();
        ingredient.Name.Should().Be("Tomate");
        ingredient.Type.Should().Be(IngredientType.Vegetable);
    }

    [Fact]
    public void Constructor_Should_TrimName()
    {
        var ingredient = new Ingredient("  Tomate  ", IngredientType.Vegetable);

        ingredient.Name.Should().Be("Tomate");
    }

    [Fact]
    public void Constructor_Should_Throw_WhenNameIsEmpty()
    {
        var act = () => new Ingredient("", IngredientType.Vegetable);

        act.Should().Throw<ArgumentException>();
    }

    [Fact]
    public void Update_Should_UpdateNameAndType_WhenValuesAreValid()
    {
        var ingredient = new Ingredient("Tomate", IngredientType.Vegetable);

        ingredient.Update("Sel", IngredientType.Spice);

        ingredient.Name.Should().Be("Sel");
        ingredient.Type.Should().Be(IngredientType.Spice);
    }
}