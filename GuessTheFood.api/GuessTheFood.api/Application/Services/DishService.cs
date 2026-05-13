using GuessTheFood.api.Application.DTOs.Dish;
using GuessTheFood.api.Application.Interfaces.Services;
using GuessTheFood.api.Application.Mappers;
using GuessTheFood.api.Domain.Entities;

namespace GuessTheFood.api.Application.Services;

public class DishService : IDishService
{
    private readonly List<Dish> _dishes = new();

    public Task<IEnumerable<DishReadDto>> GetAllAsync()
    {
        var dishes = _dishes
            .Select(DishMapper.ToReadDto);

        return Task.FromResult(dishes);
    }

    public Task<DishReadDto?> GetByIdAsync(Guid id)
    {
        var dish = _dishes.FirstOrDefault(d => d.Id == id);

        if (dish is null)
            return Task.FromResult<DishReadDto?>(null);

        return Task.FromResult<DishReadDto?>(DishMapper.ToReadDto(dish));
    }

    public Task<DishReadDto> CreateAsync(DishCreateDto dto)
    {
        var dish = DishMapper.ToEntity(dto);

        _dishes.Add(dish);

        return Task.FromResult(DishMapper.ToReadDto(dish));
    }

    public Task<bool> UpdateAsync(Guid id, DishUpdateDto dto)
    {
        var dish = _dishes.FirstOrDefault(d => d.Id == id);

        if (dish is null)
            return Task.FromResult(false);

        dish.Update(dto.Name, dto.Description);

        return Task.FromResult(true);
    }

    public Task<bool> DeleteAsync(Guid id)
    {
        var dish = _dishes.FirstOrDefault(d => d.Id == id);

        if (dish is null)
            return Task.FromResult(false);

        _dishes.Remove(dish);

        return Task.FromResult(true);
    }
}