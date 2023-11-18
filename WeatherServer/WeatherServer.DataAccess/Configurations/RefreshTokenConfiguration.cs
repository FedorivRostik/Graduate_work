using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WeatherServer.Core.Entities;

namespace WeatherServer.DataAccess.Configurations;
public class RefreshTokenConfiguration : IEntityTypeConfiguration<RefreshToken>
{
	public void Configure(EntityTypeBuilder<RefreshToken> builder)
	{
		builder
			.HasKey(x => x.Id);

		builder
			.Property(x=>x.Token)
			.IsRequired();

		builder
			.Property(x => x.JwtId)
			.IsRequired();

		builder
			.Property(x => x.DateAdded)
			.IsRequired();

		builder
			.Property(x => x.DateExpired)
			.IsRequired();
	}
}
