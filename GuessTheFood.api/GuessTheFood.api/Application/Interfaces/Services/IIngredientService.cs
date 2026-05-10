using GuessTheFood.api.Application.DTOs.Ingredients;

namespace GuessTheFood.api.Application.Interfaces.Services;

public interface IIngredientService
{
    Task<List<IngredientDto>> GetAllAsync();

    Task<IngredientDto?> GetByIdAsync(Guid id);

    Task<IngredientDto> CreateAsync(CreateIngredientDto dto);

    Task<bool> UpdateAsync(Guid id, UpdateIngredientDto dto);

    Task<bool> DeleteAsync(Guid id);
}