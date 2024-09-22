namespace qrsystem.Models.Entities;

public class User: BaseEntity
{
    public string FullName { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string PasswordHash { get; set; } = null!;
    public string? WebsiteUrl { get; set; }
    public string? ImageUrl { get; set; }
    public string? PublicKey { get; set; }
    public string? PrivateKey { get; set; }
    public string? RefreshToken { get; set; }
    public DateTime? RefreshTokenExpiry { get; set; }
}