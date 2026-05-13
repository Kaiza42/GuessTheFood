using GuessTheFood.api.Application.DTOs.Dish;

namespace GuessTheFood.api.Application.Interfaces.Services;

public interface IDishService
{
    Task<IEnumerable<DishReadDto>> GetAllAsync();

    Task<DishReadDto?> GetByIdAsync(Guid id);

    Task<DishReadDto> CreateAsync(DishCreateDto dto);

    Task<bool> UpdateAsync(Guid id, DishUpdateDto dto);

    Task<bool> DeleteAsync(Guid id);
}