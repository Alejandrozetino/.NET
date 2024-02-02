using EntityFramework.Entities;
using Microsoft.EntityFrameworkCore;

namespace EntityFramework.DbContext;

public class AppDbContext : Microsoft.EntityFrameworkCore.DbContext
{
	public AppDbContext(DbContextOptions options) : base(options)
	{

	}

	protected override void OnModelCreating(ModelBuilder modelBuilder)
	{
		base.OnModelCreating(modelBuilder);
	}

	public DbSet<Categorie> Categories { get; set; }
	public DbSet<Car> Cars { get; set; }
	public DbSet<User> Users { get; set; }
}
