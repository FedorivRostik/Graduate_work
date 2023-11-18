using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using WeatherServer.Core.Entities;

namespace WeatherServer.DataAccess;
public class WeatherDbContext : IdentityDbContext<WeatherUser>
{
    public WeatherDbContext(DbContextOptions<WeatherDbContext> options) : base(options)
	{
        
    }
    public DbSet<RefreshToken> RefreshTokens { get; set; }

	protected override void OnModelCreating(ModelBuilder modelBuilder)
	{
		base.OnModelCreating(modelBuilder);

		modelBuilder.ApplyConfigurationsFromAssembly(GetType().Assembly);
		modelBuilder.SeedRoles();
	}
}
