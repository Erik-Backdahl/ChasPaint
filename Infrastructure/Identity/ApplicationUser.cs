using Microsoft.AspNetCore.Identity;

public class ApplicationUser : IdentityUser
{
    public Guid DomainUserId { get; set; }
    public DomainUser DomainUser { get; set; } = null!;
}