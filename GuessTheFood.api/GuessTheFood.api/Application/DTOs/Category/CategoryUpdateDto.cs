using System.ComponentModel.DataAnnotations;

namespace GuessTheFood.api.Application.DTOs.Category;

public class CategoryUpdateDto
{
    [Required]
    [MinLength(2)]
    [MaxLength(30)]
    [RegularExpression(@"^[a-zA-ZÀ-ÿ\s\-']+$", ErrorMessage = "Category name can only contain letters, spaces, hyphens and apostrophes.")]
    public string Name { get; set; } = string.Empty;
}