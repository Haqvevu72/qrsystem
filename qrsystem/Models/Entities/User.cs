namespace qrsystem.Models.Entities;

public class User: BaseEntity
{
    public string FullName { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string PasswordHash { get; set; } = null!;
    public string? RefreshToken { get; set; }
    public DateTime? RefreshTokenExpiry { get; set; }
    
    // Lazy-loaded collection of QR codes
    public virtual ICollection<Qr> Qrs { get; set; }
}