using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Weather.Services.AuthApi.Dtos;
using Weather.Services.AuthApi.Dtos.Auths;
using Weather.Services.AuthApi.Dtos.Extensions;
using Weather.Services.AuthApi.Services.Interfaces;
using Weather.Services.AuthApi.Utilities.Constants;

namespace Weather.Services.AuthApi.Controllers;

[Route("api/auth")]
[ApiController]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;
    private readonly IUserService _userService;
    private ResponseDto ResponseDto { get; set; } = new();

    public AuthController(
        IAuthService authService, IUserService userService)
    {
        _authService = authService;
        _userService = userService;
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
    [Authorize(Roles =AppRoles.AdminRole)]
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
    [Authorize(Roles = AppRoles.AdminRole)]
    [HttpGet("users")]
    public async Task<IActionResult> GetUsersAsync()
    {
        return Ok(ResponseDto.SetResult(await _userService.GetAllUsersAsync()));
    }

    [Authorize]
    [HttpGet("profile")]
    public async Task<IActionResult> GetUserAsync()
    {
        var id = User.Claims.First(x => x.Type== ClaimTypes.NameIdentifier).Value!;
        return Ok(ResponseDto.SetResult(await _userService.GetUserAsync(id)));
    }

    [Authorize]
    [HttpPut("profile")]
    public async Task<IActionResult> UpdateUserAsync([FromBody] UpdateUserPersonalParamsDto dto)
    {
        var id = User.Claims.First(x => x.Type == ClaimTypes.NameIdentifier).Value!;
        return Ok(ResponseDto.SetResult(await _userService.UpdateUserPersonalParamsAsync(id,dto)));
    }
}
