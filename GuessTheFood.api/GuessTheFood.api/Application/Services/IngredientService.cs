using GuessTheFood.api.Application.DTOs.Ingredients;
using GuessTheFood.api.Application.Interfaces.Services;
using GuessTheFood.api.Application.Mappers;
using GuessTheFood.Api.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace GuessTheFood.api.Application.Services;

public class IngredientService : IIngredientService
{
    private readonly AppDbContext _context;

    public IngredientService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<List<IngredientDto>> GetAllAsync()
    {
        var ingredients = await _context.Ingredients
            .ToListAsync();

        return ingredients
            .Select(IngredientMapper.ToDto)
            .ToList();
    }

    public async Task<IngredientDto?> GetByIdAsync(Guid id)
    {
        var ingredient = await _context.Ingredients
            .FirstOrDefaultAsync(i => i.Id == id);

        if (ingredient is null)
            return null;

        return IngredientMapper.ToDto(ingredient);
    }

    public async Task<IngredientDto> CreateAsync(
        CreateIngredientDto dto)
    {
        var ingredient = IngredientMapper.ToEntity(dto);

        _context.Ingredients.Add(ingredient);

        await _context.SaveChangesAsync();

        return IngredientMapper.ToDto(ingredient);
    }

    public async Task<bool> UpdateAsync(
        Guid id,
        UpdateIngredientDto dto)
    {
        var ingredient = await _context.Ingredients
            .FirstOrDefaultAsync(i => i.Id == id);

        if (ingredient is null)
            return false;

        IngredientMapper.UpdateEntity(
            ingredient,
            dto);

        await _context.SaveChangesAsync();

        return true;
    }

    public async Task<bool> DeleteAsync(Guid id)
    {
        var ingredient = await _context.Ingredients
            .FirstOrDefaultAsync(i => i.Id == id);

        if (ingredient is null)
            return false;

        _context.Ingredients.Remove(ingredient);

        await _context.SaveChangesAsync();

        return true;
    }
}