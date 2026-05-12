using System.ComponentModel.DataAnnotations;
using GuessTheFood.api.Domain.Enums;

namespace GuessTheFood.api.Application.DTOs.Ingredients;

public class IngredientDto
{
    public Guid Id { get; set; }

    [Required]
    [MaxLength(50)]
    [MinLength(2)]
    [RegularExpression(@"^[a-zA-ZÀ-ÿ\s]+$",
        ErrorMessage = "Only letters are allowed.")]
    public string Name { get; set; } = string.Empty;

    [Required]
    public IngredientType Type { get; set; }
}