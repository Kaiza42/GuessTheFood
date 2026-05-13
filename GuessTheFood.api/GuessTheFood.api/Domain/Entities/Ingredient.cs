using GuessTheFood.api.Domain.Enums;
public class Ingredient
{
    public Guid Id { get; private set; }
    public string Name { get; private set; } = string.Empty;
    public IngredientType Type { get; private set; }
    public DateTime CreatedAt { get; private set; }

    private Ingredient() { }

    public Ingredient(string name, IngredientType type)
    {
        Id = Guid.NewGuid();
        CreatedAt = DateTime.UtcNow;
        SetName(name);
        Type = type;
    }

    public void Update(string name, IngredientType type)
    {
        SetName(name);
        Type = type;
    }

    private void SetName(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("Ingredient name cannot be empty.");

        name = name.Trim();

        if (name.Length < 2 || name.Length > 50)
            throw new ArgumentException("Ingredient name must be between 2 and 50 characters.");

        Name = name;
    }
}