using Microsoft.AspNetCore.Mvc;
using Weather.Services.AuthApi.Dtos;
using Weather.Services.AuthApi.Dtos.Auths;
using Weather.Services.AuthApi.Dtos.Extensions;
using Weather.Services.AuthApi.Services.Interfaces;

namespace Weather.Services.AuthApi.Controllers;

[Route("api/auth")]
[ApiController]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;
    private ResponseDto ResponseDto { get; set; } = new();

    public AuthController(
        IAuthService authService)
    {
        _authService = authService;
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] RegisterRequestDto model)
    {
        var responseStatus = await _authService.Register(model);
        if (!responseStatus)
        {
            return BadRequest(ResponseDto.SetResult(responseStatus));
        }

        return Ok(ResponseDto.SetResult(responseStatus));
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginRequestDto loginRequest)
    {

        return Ok(ResponseDto.SetResult(await _authService.Login(loginRequest)));
    }

    [HttpPost("assignRole")]
    public async Task<IActionResult> AssignRole([FromBody] RegisterRequestDto registerRequestDto)
    {
        var responseStatus = await _authService.AssignRole(registerRequestDto.Email, registerRequestDto.Role!.ToUpper());

        if (!responseStatus)
        {
            return BadRequest(ResponseDto.SetResult(responseStatus));
        }

        return Ok(ResponseDto.SetResult(responseStatus));

    }
}
