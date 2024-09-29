namespace qrsystem.Models.Dtos.Qr;

public class QrGet
{
    public string Id { get; set; }
    public string Value { get; set; }
    public string Title { get; set; }
    public string BgColor { get; set; }
    public string FgColor { get; set; }
    public string ImgUrl { get; set; }
    public int  Scans { get; set; }
    
    // Reference to the user who generated this QR code
    public string UserId { get; set; }
}