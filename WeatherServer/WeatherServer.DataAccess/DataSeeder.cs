using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using WeatherServer.Core.Constants;

namespace WeatherServer.DataAccess;
public static class DataSeeder
{
	public static void SeedRoles(this ModelBuilder modelBuilder)
	{
		modelBuilder.Entity<IdentityRole>().HasData(new[]
		{
			new IdentityRole() { Id= "2c5e174e-3b0e-446f-86af-483d56fd7210",Name = UserRoles.Admin, NormalizedName = UserRoles.Admin.ToUpper() },
			new IdentityRole() { Id= "2c5e174e-3b0e-446f-86af-483d56fd7211",Name = UserRoles.Manager, NormalizedName = UserRoles.Manager.ToUpper() },
			new IdentityRole() { Id= "2c5e174e-3b0e-446f-86af-483d56fd7212",Name = UserRoles.Customer, NormalizedName = UserRoles.Customer.ToUpper() }
		});
	}
}
