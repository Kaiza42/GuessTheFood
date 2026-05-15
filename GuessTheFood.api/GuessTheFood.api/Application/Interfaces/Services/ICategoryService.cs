using GuessTheFood.api.Application.DTOs.Category;

namespace GuessTheFood.api.Application.Interfaces.Services;

public interface ICategoryService
{
    Task<List<CategoryReadDto>> GetAllAsync();
    Task<CategoryReadDto?> GetByIdAsync(Guid id);
    Task<CategoryReadDto?> GetByNameAsync(string name);
    Task<CategoryReadDto> CreateAsync(CategoryCreateDto dto);
    Task<bool> UpdateAsync(Guid id, CategoryUpdateDto dto);
    Task<bool> DeleteAsync(Guid id);
}