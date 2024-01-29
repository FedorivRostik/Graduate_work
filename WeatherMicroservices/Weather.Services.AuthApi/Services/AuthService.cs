using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Weather.Services.AuthApi.Data;
using Weather.Services.AuthApi.Dtos;
using Weather.Services.AuthApi.Dtos.Auths;
using Weather.Services.AuthApi.Models;
using Weather.Services.AuthApi.Services.Interfaces;

namespace Weather.Services.AuthApi.Services;

public class AuthService : IAuthService
{
    private readonly AppDbContext _db;
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly RoleManager<IdentityRole> _roleManager;
    private readonly IJwtTokenGenerator _tokenGenerator;

    public AuthService(
        AppDbContext appDbContext,
        UserManager<ApplicationUser> userManager,
        RoleManager<IdentityRole> roleManager,
        IJwtTokenGenerator tokenGenerator)
    {
        _db = appDbContext;
        _userManager = userManager;
        _roleManager = roleManager;
        _tokenGenerator = tokenGenerator;
    }

    public async Task<LoginResponseDto> Login(LoginRequestDto loginRequestDto)
    {
        var user = await _db.ApplicationUsers
            .FirstOrDefaultAsync(u => u.UserName!.ToLower() == loginRequestDto.UserName.ToLower());

        if (user is null )
        {
            throw new InvalidOperationException("Unvalid credentionals");
        }

        bool isValid = await _userManager.CheckPasswordAsync(user, loginRequestDto.Password);

        if (!isValid)
        {
            throw new InvalidOperationException("Unvalid credentionals");
        }

        UserDto userDto = new UserDto()
        {
            Email = user.Email!,
            UserId = user.Id,
            PhoneNumber = user.PhoneNumber!,
            Name = user.Name,
        };
        LoginResponseDto loginResponseDto = new LoginResponseDto()
        {
            User = userDto,
            Token = _tokenGenerator.GenerateToken(user, await _userManager.GetRolesAsync(user)),
        };
        return loginResponseDto;
    }

    public async Task<bool> Register(RegisterRequestDto registerRequestDto)
    {
        ApplicationUser user = new()
        {
            UserName = registerRequestDto.Email,
            Email = registerRequestDto.Email,
            NormalizedEmail = registerRequestDto.Email.ToUpper(),
            Name = registerRequestDto.Name,
            PhoneNumber = registerRequestDto.PhoneNumber,

        };

        var result = await _userManager.CreateAsync(user, registerRequestDto.Password);

        if (!result.Succeeded)
        {
            return false;
        }

        return true;
    }

    public async Task<bool> AssignRole(string email, string roleName)
    {
        var userToReturn = await _db.ApplicationUsers
            .FirstOrDefaultAsync(u => u.Email!.ToLower() == email.ToLower());

        if (userToReturn != null && await _roleManager.RoleExistsAsync(roleName))
        {
            await _userManager.AddToRoleAsync(userToReturn, roleName);
            return true;
        }
        return false;
    }
}
