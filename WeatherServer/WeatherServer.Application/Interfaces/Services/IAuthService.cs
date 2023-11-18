using Microsoft.AspNetCore.Mvc;
using WeatherServer.Shared.Dtos.User;

namespace WeatherServer.Application.Interfaces.Services;
public interface IAuthService
{
	Task<IActionResult> RegisterAsync(UserRegisterDto userDto);
	Task<IActionResult> LoginUserAsync([FromBody] UserLoginDto loginDto);
	Task<UserAuthResultDto> VerifyAndGenerateTokenAsync(UserRefreshTokenDto userRefreshTokenDto);
}
