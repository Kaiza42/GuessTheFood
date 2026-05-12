using System.ComponentModel.DataAnnotations;
using GuessTheFood.api.Domain.Enums;

namespace GuessTheFood.api.Application.DTOs.Ingredients;

public class UpdateIngredientDto
{
    [Required]
    [MaxLength(50)]
    [RegularExpression(@"^[a-zA-ZÀ-ÿ\s]+$",
        ErrorMessage = "Ingredient name can only contain letters and spaces.")]
    public string Name { get; set; } = string.Empty;

    [Required]
    [EnumDataType(typeof(IngredientType))]
    public IngredientType Type { get; set; }
}