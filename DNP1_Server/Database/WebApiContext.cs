using DNP1_Server.Utils;
using Microsoft.EntityFrameworkCore;

namespace DNP1_Server.Database; 

public class WebApiContext : DbContext {
	public DbSet<User> Users { get; set; }
	public DbSet<DbPost> Posts { get; set; }

	protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) {
		optionsBuilder.UseSqlite("Data Source = WebDatabase.db");
	}
	
	protected override void OnModelCreating(ModelBuilder modelBuilder) {
		modelBuilder.Entity<DbPost>().HasKey(post => post.Id);
		modelBuilder.Entity<User>().HasKey(user => user.Username);
	}
}