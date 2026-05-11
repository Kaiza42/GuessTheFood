using System.ComponentModel.DataAnnotations;

namespace GuessTheFood.api.Application.DTOs.Dish;

public class DishUpdateDto
{
    [Required]
    [MinLength(2)]
    [MaxLength(50)]
    public string Name { get; set; } = string.Empty;
}