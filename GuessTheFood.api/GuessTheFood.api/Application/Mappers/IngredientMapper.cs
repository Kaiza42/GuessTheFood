using GuessTheFood.api.Application.DTOs.Ingredients;
using GuessTheFood.api.Domain.Entities;

namespace GuessTheFood.api.Application.Mappers;

public static class IngredientMapper
{
    public static IngredientDto ToDto(Ingredient ingredient)
    {
        return new IngredientDto
        {
            Id = ingredient.Id,
            Name = ingredient.Name,
            Type = ingredient.Type
        };
    }

    public static Ingredient ToEntity(CreateIngredientDto dto)
    {
        return new Ingredient(dto.Name, dto.Type);
    }

    public static void UpdateEntity(Ingredient ingredient, UpdateIngredientDto dto)
    {
        ingredient.Update(dto.Name, dto.Type);
    }
}