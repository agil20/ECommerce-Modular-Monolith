namespace Modules.Identity.Domain;

public class RefreshToken
{
    public Guid Id { get; set; }
    public string UserId { get; set; } = string.Empty;

    // Only a SHA-256 hash is stored, so a leaked database does not expose usable tokens.
    public string TokenHash { get; set; } = string.Empty;

    public DateTime CreatedAt { get; set; }
    public DateTime ExpiresAt { get; set; }
    public DateTime? RevokedAt { get; set; }
    public string? ReplacedByTokenHash { get; set; }
}
