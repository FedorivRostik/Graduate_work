using Microsoft.AspNetCore.Identity;

namespace WeatherServer.Core.Entities;
public class WeatherUser : IdentityUser
{
	public IList<RefreshToken> RefreshTokens { get; set; } = new List<RefreshToken>();
}
