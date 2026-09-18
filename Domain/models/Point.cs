public partial class Point
{
    public int Id { get; set; }
    public int YCoordinate { get; set; }
    public int XCoordinate { get; set; }
    public string? ColorHex { get; set; }
    public DomainUser? Owner { get; set; } 
}