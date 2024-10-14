namespace qrsystem.Models.Entities;

public class Qr: BaseEntity
{
    public string Value { get; set; }
    public string Title { get; set; }
    public string BgColor { get; set; }
    public string FgColor { get; set; } = "#000000";
    public string ImgUrl { get; set; }
    public int Scans { get; set; }
    
    // Reference to the user who generated this QR code
    public string UserId { get; set; }
    public virtual User User { get; set; }
}