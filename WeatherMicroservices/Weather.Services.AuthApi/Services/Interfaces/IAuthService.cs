using Weather.Services.AuthApi.Dtos.Auths;

namespace Weather.Services.AuthApi.Services.Interfaces;

public interface IAuthService
{
    Task<bool> Register(RegisterRequestDto registerRequestDto);
    Task<LoginResponseDto> Login(LoginRequestDto loginRequestDto);
    Task<bool> AssignRole(string email, string roleName);
}