using System.ComponentModel.DataAnnotations;

namespace GuessTheFood.api.Application.DTOs.Ingredients;

public class IngredientDto
{
    public Guid Id { get; set; }

    [Required]
    [MaxLength(50)]
    [RegularExpression(@"^[a-zA-ZÀ-ÿ\s]+$",
        ErrorMessage = "Only letters are allowed.")]
    public string Name { get; set; } = string.Empty;
}