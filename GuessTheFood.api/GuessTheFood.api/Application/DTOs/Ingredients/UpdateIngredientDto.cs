using System.ComponentModel.DataAnnotations;

namespace GuessTheFood.api.Application.DTOs.Ingredients;

public class UpdateIngredientDto
{
    [Required]
    [MaxLength(50)]
    [MinLength(2)]
    [RegularExpression(@"^[a-zA-ZÀ-ÿ\s]+$",
        ErrorMessage = "Ingredient name can only contain letters and spaces.")]
    public string Name { get; set; } = string.Empty;
}