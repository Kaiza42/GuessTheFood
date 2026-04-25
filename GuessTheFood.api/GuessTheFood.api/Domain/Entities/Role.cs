namespace GuessTheFood.Api.Domain.Entities;

public class Role
{
    public Guid Id { get; private set; }
    public string Name { get; private set; }

    private Role()
    {
        Name = string.Empty;
    }

    public Role(string name)
    {
        Id = Guid.NewGuid();

        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("Role name cannot be empty.", nameof(name));

        Name = name.Trim();
    }
}