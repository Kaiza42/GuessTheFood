using GuessTheFood.api.Application.DTOs.Category;
using GuessTheFood.api.Application.Interfaces.Services;
using GuessTheFood.api.Application.Mappers;
using GuessTheFood.api.Domain.Entities;
using GuessTheFood.Api.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace GuessTheFood.api.Application.Services;

public class CategoryService : ICategoryService
{
    private readonly AppDbContext _context;

    public CategoryService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<List<CategoryReadDto>> GetAllAsync()
    {
        return await _context.Categories
            .AsNoTracking()
            .OrderBy(category => category.Name)
            .Select(category => CategoryMapper.ToReadDto(category))
            .ToListAsync();
    }

    public async Task<CategoryReadDto?> GetByIdAsync(Guid id)
    {
        Category? category = await _context.Categories
            .AsNoTracking()
            .FirstOrDefaultAsync(category => category.Id == id);

        return category is null
            ? null
            : CategoryMapper.ToReadDto(category);
    }

    public async Task<CategoryReadDto?> GetByNameAsync(string name)
    {
        string normalizedName = name.Trim();

        Category? category = await _context.Categories
            .AsNoTracking()
            .FirstOrDefaultAsync(category => category.Name == normalizedName);

        return category is null
            ? null
            : CategoryMapper.ToReadDto(category);
    }

    public async Task<CategoryReadDto> CreateAsync(CategoryCreateDto dto)
    {
        string normalizedName = dto.Name.Trim();

        bool alreadyExists = await _context.Categories
            .AnyAsync(category => category.Name == normalizedName);

        if (alreadyExists)
            throw new InvalidOperationException("Category already exists.");

        Category category = CategoryMapper.ToEntity(dto);

        _context.Categories.Add(category);
        await _context.SaveChangesAsync();

        return CategoryMapper.ToReadDto(category);
    }

    public async Task<bool> UpdateAsync(Guid id, CategoryUpdateDto dto)
    {
        Category? category = await _context.Categories
            .FirstOrDefaultAsync(category => category.Id == id);

        if (category is null)
            return false;

        string normalizedName = dto.Name.Trim();

        bool alreadyExists = await _context.Categories
            .AnyAsync(existingCategory =>
                existingCategory.Id != id &&
                existingCategory.Name == normalizedName);

        if (alreadyExists)
            throw new InvalidOperationException("Category already exists.");

        category.UpdateName(dto.Name);

        await _context.SaveChangesAsync();

        return true;
    }

    public async Task<bool> DeleteAsync(Guid id)
    {
        Category? category = await _context.Categories
            .FirstOrDefaultAsync(category => category.Id == id);

        if (category is null)
            return false;

        _context.Categories.Remove(category);
        await _context.SaveChangesAsync();

        return true;
    }
}