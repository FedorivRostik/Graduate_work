namespace Weather.Services.AuthApi.Dtos.Auths;

public class LoginRequestDto
{
    public string UserName { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
}
