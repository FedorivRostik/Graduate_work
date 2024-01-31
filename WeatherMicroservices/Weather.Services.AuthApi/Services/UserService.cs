using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Weather.Services.AuthApi.Data;
using Weather.Services.AuthApi.Dtos;
using Weather.Services.AuthApi.Models;
using Weather.Services.AuthApi.Services.Interfaces;

namespace Weather.Services.AuthApi.Services;

public class UserService : IUserService
{
    private readonly AppDbContext _db;
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly RoleManager<IdentityRole> _roleManager;

    public UserService(
        AppDbContext appDbContext,
        UserManager<ApplicationUser> userManager,
        RoleManager<IdentityRole> roleManager
     )
    {
        _db = appDbContext;
        _userManager = userManager;
        _roleManager = roleManager;
    }

    public async Task<List<UserDto>> GetAllUsersAsync()
    {
        var users = await _userManager.Users.ToListAsync();

        return users 
            .Select( x => new UserDto(x,  _userManager
                .GetRolesAsync(x).Result
                .FirstOrDefault()!))
            .ToList();
    }
}
