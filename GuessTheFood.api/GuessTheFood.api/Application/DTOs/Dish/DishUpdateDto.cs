using System.ComponentModel.DataAnnotations;

namespace GuessTheFood.api.Application.DTOs.Dish;

public class DishUpdateDto
{
    [Required]
    [MinLength(2)]
    [MaxLength(50)]
    [RegularExpression(@"^[a-zA-ZÀ-ÿ\s]+$",
        ErrorMessage = "Name must contain only letters.")]
    public string Name { get; set; } = string.Empty;
    
    [Required]
    [MinLength(30)]
    [MaxLength(250)]
    public string Description { get; set; } = string.Empty;
}