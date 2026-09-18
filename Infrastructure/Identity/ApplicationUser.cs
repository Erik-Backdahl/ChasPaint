using Microsoft.AspNetCore.Identity;

public class ApplicationUser : IdentityUser
{
    public int DomainUserId { get; set; }
    public DomainUser DomainUser { get; set; } = null!;
}