namespace qrsystem.Models.Entities;

public class Qr : BaseEntity
{
    public string Value { get; set; } = null!;
    public string Title { get; set; } = null!;
    public string BgColor { get; set; } = null!;
    public string FgColor { get; set; } = "#000000";
    public string? ImgUrl { get; set; }
    public int Scans { get; set; }
    public string UserId { get; set; } = null!;
    public virtual User User { get; set; } = null!;
}