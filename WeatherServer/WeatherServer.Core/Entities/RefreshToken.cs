namespace WeatherServer.Core.Entities;
public class RefreshToken
{
	public Guid Id { get; set; } = Guid.NewGuid();

	public string Token { get; set; }
	public string JwtId { get; set; }
	public bool IsRevoked { get; set; } = false;
	public DateTime DateAdded { get; set; }
	public DateTime DateExpired { get; set; }

	public string WeatherUserId { get; set; }
	public WeatherUser WeatherUser { get; set; }
}
