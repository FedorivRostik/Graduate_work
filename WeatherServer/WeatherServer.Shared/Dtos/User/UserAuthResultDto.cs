namespace WeatherServer.Shared.Dtos.User;
public class UserAuthResultDto
{
	public string Token { get; set; }
	public string RefreshToken { get; set; }
	public DateTime ExpiresAt { get; set; }
}
