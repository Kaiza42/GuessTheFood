namespace GuessTheFood.api.Domain.Entities;

public class Ingredient
{
    public Guid Id { get; private set; }
    public string Name { get; private set; }

    private Ingredient() { }

    public Ingredient(string name)
    {
        Id = Guid.NewGuid();
        Name = ValidateName(name);
    }

    private static string ValidateName(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("Ingredient name cannot be empty.", nameof(name));

        return name.Trim().ToLowerInvariant();
    }
}