namespace GuessTheFood.api.Domain.Entities;

public class Category
{
    public Guid Id { get; private set; }

    public string Name { get; private set; }

    private Category()
    {
        Name = string.Empty;
    }

    public Category(string name)
    {
        Id = Guid.NewGuid();
        Name = ValidateName(name);
    }

    public void ChangeName(string name)
    {
        Name = ValidateName(name);
    }

    private static string ValidateName(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("Category name cannot be empty.");

        name = name.Trim();

        if (name.Length < 2 || name.Length > 20)
            throw new ArgumentException("Category name must be between 2 and 20 characters.");

        return name;
    }
}