namespace Weather.Services.AuthApi.Dtos.Auths;

public class LoginResponseDto
{
    
    public UserDto? User { get; set; }
    public string Token { get; set; } = string.Empty;
}

