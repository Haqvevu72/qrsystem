namespace qrsystem.Models.Dtos.Qr;

public class QrAdd
{
    public string Value { get; set; }
    public string Title { get; set; }
    public string BgColor { get; set; }
    public string FgColor { get; set; }
    public string? ImgUrl { get; set; }
    
    // Reference to the user who generated this QR code
    public string UserId { get; set; }
}