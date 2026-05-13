namespace GuessTheFood.api.Domain.Entities;

public class Country
{
    public Guid Id { get; private set; }

    public string Name { get; private set; }

    private Country()
    {
        Name = string.Empty;
    }

    public Country(string name)
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
            throw new ArgumentException("Country name cannot be empty.");

        name = name.Trim();

        if (name.Length < 2 || name.Length > 100)
            throw new ArgumentException("Country name must be between 2 and 100 characters.");

        return name;
    }
}