namespace qrsystem.Models.Entities;

public class Qr: BaseEntity
{
    public int Scans { get; set; }
    public string Value { get; set; }
    public string Title { get; set; }
    public string BgColor { get; set; }
    public string FgColor { get; set; }
}