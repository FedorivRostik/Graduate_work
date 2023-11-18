using System.ComponentModel.DataAnnotations;

namespace WeatherServer.Shared.Dtos.User;
public class UserRefreshTokenDto
{
	[Required]
	public string Token { get; set; }
	[Required]
	public string RefreshToken { get; set; }
}
