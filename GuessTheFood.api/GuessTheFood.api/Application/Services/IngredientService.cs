using GuessTheFood.api.Application.DTOs.Ingredients;
using GuessTheFood.api.Application.Interfaces.Services;
using GuessTheFood.api.Application.Mappers;
using GuessTheFood.Api.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using GuessTheFood.api.Domain.Enums;

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
    
    public async Task<List<IngredientDto>> GetByTypeAsync(IngredientType type)
    {
        var ingredients = await _context.Ingredients
            .Where(i => i.Type == type)
            .ToListAsync();

        return ingredients.Select(i => new IngredientDto
        {
            Id = i.Id,
            Name = i.Name,
            Type = i.Type
        }).ToList();
    }

    public async Task<IngredientDto> CreateAsync(CreateIngredientDto dto)
    {
        var name = dto.Name.Trim();

        var exists = await _context.Ingredients
            .AnyAsync(i => i.Name.ToLower() == name.ToLower());

        if (exists)
            throw new InvalidOperationException("Ingredient already exists.");

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

        var name = dto.Name.Trim();

        var exists = await _context.Ingredients
            .AnyAsync(i =>
                i.Id != id &&
                i.Name.ToLower() == name.ToLower());

        if (exists)
            throw new InvalidOperationException("Ingredient already exists.");

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
    
    public async Task<IngredientDto?> GetByNameAsync(string name)
    {
        var ingredient = await _context.Ingredients
            .FirstOrDefaultAsync(i =>
                i.Name.ToLower() == name.Trim().ToLower());

        if (ingredient is null)
            return null;

        return IngredientMapper.ToDto(ingredient);
    }
}