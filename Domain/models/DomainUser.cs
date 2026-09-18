using System.Dynamic;

public partial class DomainUser
{
    public int Id { get; set; }
    public List<Point> Points { get; set; } = new();
    public string UserName { get; set; } = null!;
}