using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WeatherServer.Core.Entities;

namespace WeatherServer.DataAccess.Configurations;
public class WeatherUserConfiguration : IEntityTypeConfiguration<WeatherUser>
{
	public void Configure(EntityTypeBuilder<WeatherUser> builder)
	{
		builder
		   .HasMany(x => x.RefreshTokens)
		   .WithOne(x => x.WeatherUser)
		   .HasForeignKey(x => x.WeatherUserId);
	}
}
