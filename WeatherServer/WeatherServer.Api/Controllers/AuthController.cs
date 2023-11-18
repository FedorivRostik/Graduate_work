using Microsoft.AspNetCore.Mvc;
using WeatherServer.Application.Interfaces.Services;
using WeatherServer.Shared.Dtos.User;

namespace WeatherServer.Api.Controllers;
[ApiController]
[Route("api/auth")]
public class AuthController : ControllerBase
{
	private readonly IAuthService _authService;
	public AuthController(IAuthService authService)
	{
		_authService = authService;
	}

	[HttpPost("register-user")]
	public async Task<IActionResult> CreateUserAsync([FromBody] UserRegisterDto registerDto)
	{
		return await _authService.RegisterAsync(registerDto);
	}

	[HttpPost("login-user")]
	public async Task<IActionResult> LoginUserAsync([FromBody] UserLoginDto loginDto)
	{
		return await _authService.LoginUserAsync(loginDto);
	}

	[HttpPost("refresh-token")]
	public async Task<IActionResult> RefreshTokenAsync([FromBody] UserRefreshTokenDto userRefreshTokenDto)
	{
		if (!ModelState.IsValid)
		{
			return BadRequest("Please, provide all required fields");
		}

		var result = await _authService.VerifyAndGenerateTokenAsync(userRefreshTokenDto);
		return Ok(result);
	}
}
