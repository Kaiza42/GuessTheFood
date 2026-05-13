namespace GuessTheFood.api.Application.DTOs.Dish;

public class DishReadDto
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Description {get; set;} = string.Empty;
}