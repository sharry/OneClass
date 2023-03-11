using Microsoft.EntityFrameworkCore;
using OneClass.Domain.Classes;
using OneClass.Domain.Users;

namespace OneClass.Infrastructure.Data;
public class OneClassDbContext : DbContext
{
	public OneClassDbContext(DbContextOptions<OneClassDbContext> options)
		: base(options)
	{
	}

	public DbSet<Class> Classes { get; set; } = null!;
	public DbSet<User> Users { get; set; } = null!;
}
