namespace WeatherServer.Shared.Dtos.User;
public class UserRegisterDto
{
    public string Email { get; set; }
    public string Password { get; set; }
    public string UserName { get; set; }
    public string? Role { get; set; }
}
