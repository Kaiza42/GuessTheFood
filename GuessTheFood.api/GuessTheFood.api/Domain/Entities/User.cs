namespace GuessTheFood.Api.Domain.Entities;

public class User
{
    public Guid Id { get; private set; }

    public string Username { get; private set; }
    public string Email { get; private set; }
    public string PasswordHash { get; private set; }

    public int TotalScore { get; private set; }

    public Guid RoleId { get; private set; }
    public Role Role { get; private set; }

    public DateTime CreatedAt { get; private set; }

    private User()
    {
        Username = string.Empty;
        Email = string.Empty;
        PasswordHash = string.Empty;
        Role = null!;
    }

    public User(
        string username,
        string email,
        string passwordHash,
        Role role)
    {
        Id = Guid.NewGuid();

        Username = ValidateUsername(username);
        Email = ValidateEmail(email);
        PasswordHash = ValidatePasswordHash(passwordHash);

        Role = role ?? throw new ArgumentNullException(nameof(role));
        RoleId = role.Id;

        TotalScore = 0;
        CreatedAt = DateTime.UtcNow;
    }

    public void AddScore(int score)
    {
        if (score < 0)
            throw new ArgumentException("Score cannot be negative.", nameof(score));

        TotalScore += score;
    }

    public void ChangeUsername(string username)
    {
        Username = ValidateUsername(username);
    }

    public void ChangeRole(Role role)
    {
        Role = role ?? throw new ArgumentNullException(nameof(role));
        RoleId = role.Id;
    }

    private static string ValidateUsername(string username)
    {
        if (string.IsNullOrWhiteSpace(username))
            throw new ArgumentException("Username cannot be empty.", nameof(username));

        return username.Trim();
    }

    private static string ValidateEmail(string email)
    {
        if (string.IsNullOrWhiteSpace(email))
            throw new ArgumentException("Email cannot be empty.", nameof(email));

        return email.Trim().ToLowerInvariant();
    }

    private static string ValidatePasswordHash(string hash)
    {
        if (string.IsNullOrWhiteSpace(hash))
            throw new ArgumentException("Password hash cannot be empty.", nameof(hash));

        return hash;
    }
}