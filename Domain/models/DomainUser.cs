using System.ComponentModel.DataAnnotations;
using System.Dynamic;

public partial class DomainUser
{
    [Key]
    public Guid Id { get; set; }
    public List<Point> Points { get; set; } = new();
    public string UserName { get; set; } = null!;
}