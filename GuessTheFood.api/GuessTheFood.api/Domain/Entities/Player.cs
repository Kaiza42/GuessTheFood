
namespace GuessTheFood.Api.Domain.Entities;

public class Player
{
    public Guid Id { get; private set; }

    public string Username { get; private set; }
    public string Email { get; private set; }

    public string PasswordHash { get; private set; }

    public int TotalScore { get; private set; }

    public DateTime CreatedAt { get; private set; }

    private Player()
    {
        Username = string.Empty;
        Email = string.Empty;
        PasswordHash = string.Empty;
    }

    public Player(
        string username,
        string email,
        string passwordHash)
    {
        Id = Guid.NewGuid();

        Username = ValidateUsername(username);
        Email = ValidateEmail(email);

        PasswordHash = ValidatePasswordHash(passwordHash);

        TotalScore = 0;

        CreatedAt = DateTime.UtcNow;
    }

    public void AddScore(int score)
    {
        if (score < 0)
            throw new ArgumentException(
                "Score cannot be negative.",
                nameof(score));

        TotalScore += score;
    }

    public void ChangeUsername(string username)
    {
        Username = ValidateUsername(username);
    }

    private static string ValidateUsername(string username)
    {
        if (string.IsNullOrWhiteSpace(username))
            throw new ArgumentException(
                "Username cannot be empty.",
                nameof(username));

        return username.Trim();
    }

    private static string ValidateEmail(string email)
    {
        if (string.IsNullOrWhiteSpace(email))
            throw new ArgumentException(
                "Email cannot be empty.",
                nameof(email));

        return email.Trim().ToLowerInvariant();
    }

    private static string ValidatePasswordHash(string hash)
    {
        if (string.IsNullOrWhiteSpace(hash))
            throw new ArgumentException(
                "Password hash cannot be empty.",
                nameof(hash));

        return hash;
    }
}