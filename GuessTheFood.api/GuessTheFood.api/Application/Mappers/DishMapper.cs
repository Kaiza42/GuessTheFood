using GuessTheFood.api.Application.DTOs.Dish;
using GuessTheFood.api.Domain.Entities;

namespace GuessTheFood.api.Application.Mappers;

public static class DishMapper
{
    public static DishReadDto ToReadDto(Dish dish)
    {
        return new DishReadDto
        {
            Id = dish.Id,
            Name = dish.Name,
            Description = dish.Description
        };
    }

    public static Dish ToEntity(DishCreateDto dto)
    {
        return new Dish(dto.Name, dto.Description);
    }
}