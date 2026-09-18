using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;


public class ChasPaintDbContext : IdentityDbContext<ApplicationUser>
{
	public ChasPaintDbContext(DbContextOptions<ChasPaintDbContext> options) : base(options) { }
	public DbSet<DomainUser> DomainUsers { get; set; }
	public DbSet<ApplicationUser> ApplicationUsers { get; set; }
	public DbSet<Point> Points { get; set; }
	protected override void OnModelCreating(ModelBuilder builder)
	{
		base.OnModelCreating(builder);

		builder.Entity<ApplicationUser>()
		.HasOne(a => a.DomainUser)
		.WithOne()
		.HasForeignKey<ApplicationUser>("DomainUserId");

		builder.Entity<Point>()
		.HasKey(p => new { p.XCoordinate, p.YCoordinate });
	}
}