using GuessTheFood.api.Application.DTOs.Category;
using GuessTheFood.api.Domain.Entities;

namespace GuessTheFood.api.Application.Mappers;

public static class CategoryMapper
{
    public static CategoryReadDto ToReadDto(Category category)
    {
        return new CategoryReadDto
        {
            Id = category.Id,
            Name = category.Name
        };
    }

    public static Category ToEntity(CategoryCreateDto dto)
    {
        return new Category(dto.Name);
    }
}