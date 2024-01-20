using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using WeatherServer.Application.Interfaces.Services;
using WeatherServer.Core.Constants;
using WeatherServer.Core.Entities;
using WeatherServer.DataAccess;
using WeatherServer.Shared.Dtos.User;

namespace WeatherServer.Application.Services;

public class AuthService : IAuthService
{
	private readonly UserManager<WeatherUser> _userManager;
	private readonly RoleManager<IdentityRole> _roleManager;
	private readonly WeatherDbContext _context;
	private readonly IConfiguration _configuration;
	private readonly TokenValidationParameters _tokenValidationParameters;

	public AuthService(
		UserManager<WeatherUser> userManager,
		RoleManager<IdentityRole> roleManager,
		WeatherDbContext context,
		IConfiguration configuration,
		TokenValidationParameters tokenValidationParameters)
	{
		_userManager = userManager;
		_roleManager = roleManager;
		_context = context;
		_configuration = configuration;
		_tokenValidationParameters = tokenValidationParameters;
	}

	public async Task<IActionResult> LoginUserAsync([FromBody] UserLoginDto loginDto)
	{
		var userExists = await _userManager.FindByEmailAsync(loginDto.Email);
		if (userExists != null && await _userManager.CheckPasswordAsync(userExists, loginDto.Password))
		{
			var tokenValue = await GenerateJWTTokenAsync(userExists);
			return new OkObjectResult(tokenValue);
		}
		return new UnauthorizedObjectResult("User invalid credentials");
	}

	public async Task<IActionResult> RegisterAsync(UserRegisterDto userDto)
	{
		var userExists = await _userManager.FindByEmailAsync(userDto.Email);
		if (userExists != null)
		{
			return new BadRequestObjectResult($"User {userDto.Email} already exists");
		}

		var newUser = new WeatherUser()
		{
			Email = userDto.Email,
			UserName = userDto.UserName,
			SecurityStamp = Guid.NewGuid().ToString()
		};

		var result = await _userManager.CreateAsync(newUser, userDto.Password);

		if (result.Succeeded)
		{
			userDto.Role ??= UserRoles.Customer;
			//add role
			var roleExist = await _roleManager.RoleExistsAsync(userDto.Role );
			if (roleExist)
			{
				await _userManager.AddToRoleAsync(newUser, userDto.Role);
			}
			return new OkObjectResult("User created");
		};
		return new BadRequestObjectResult("User could not be created");
	}

		public async Task<UserAuthResultDto> VerifyAndGenerateTokenAsync(UserRefreshTokenDto tokenRequestVM)
		{
			var jwtTokenHandler = new JwtSecurityTokenHandler();
			var storedToken = await _context.RefreshTokens.FirstOrDefaultAsync(x => x.Token == tokenRequestVM.RefreshToken);
			var dbUser = await _userManager.FindByIdAsync(storedToken.WeatherUserId);

			try
			{
				var tokenCheckResult = jwtTokenHandler.ValidateToken(tokenRequestVM.Token, _tokenValidationParameters, out var validatedToken);

				return await GenerateJWTTokenAsync(dbUser, storedToken);
			}
			catch (SecurityTokenExpiredException)
			{
				if (storedToken.DateExpired >= DateTime.UtcNow)
				{
					return await GenerateJWTTokenAsync(dbUser, storedToken);
				}
				else
				{
					return await GenerateJWTTokenAsync(dbUser);
				}
			}
		}

	private async Task<UserAuthResultDto> GenerateJWTTokenAsync(WeatherUser user, RefreshToken rToken = default!)
	{
		var authClaims = new List<Claim>()
			{
				new Claim(ClaimTypes.Name, user.UserName),
				new Claim(ClaimTypes.NameIdentifier, user.Id),
				new Claim(JwtRegisteredClaimNames.Email, user.Email),
				new Claim(JwtRegisteredClaimNames.Sub, user.Email),
				new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
			};

		var userRoels = await _userManager.GetRolesAsync(user);

		foreach (var role in userRoels)
		{
			authClaims.Add(new Claim(ClaimTypes.Role, role));
		}

		var authSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_configuration["JWT:Secret"]));

		var token = new JwtSecurityToken(
			issuer: _configuration["JWT:Issuer"],
			audience: _configuration["JWT:Audience"],
			expires: DateTime.UtcNow.AddMinutes(1),
			claims: authClaims,
			signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256));

		var jwtToken = new JwtSecurityTokenHandler().WriteToken(token);

		if (rToken != null)
		{
			var rTokenResponse = new UserAuthResultDto()
			{
				Token = jwtToken,
				RefreshToken = rToken.Token,
				ExpiresAt = token.ValidTo
			};
			return rTokenResponse;
		}

		var refreshToken = new RefreshToken()
		{
			JwtId = token.Id,
			IsRevoked = false,
			WeatherUserId = user.Id,
			DateAdded = DateTime.UtcNow,
			DateExpired = DateTime.UtcNow.AddMonths(6),
			Token = Guid.NewGuid().ToString() + "-" + Guid.NewGuid().ToString()
		};
		await _context.RefreshTokens.AddAsync(refreshToken);
		await _context.SaveChangesAsync();


		var response = new UserAuthResultDto()
		{
			Token = jwtToken,
			RefreshToken = refreshToken.Token,
			ExpiresAt = token.ValidTo
		};

		return response;

	}

	
}
