using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Weather.Services.AuthApi.Data;
using Weather.Services.AuthApi.Dtos;
using Weather.Services.AuthApi.Enums;
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

    public async Task<UserDto> GetUserAsync(string id)
    {
        var user = await _userManager.Users.FirstOrDefaultAsync(x => x.Id == id);
        var role =  _userManager
                .GetRolesAsync(user).Result
                .FirstOrDefault()!;

        return new UserDto(user, role);
    }

    public async Task<UserDto> UpdateUserPersonalParamsAsync(string id, UpdateUserPersonalParamsDto dto)
    {
        var user = await _userManager.Users.FirstOrDefaultAsync(x => x.Id == id);
        user.Weight = dto.Weight;
        user.Age = dto.Age;
        user.Region = dto.Region;
        user.City = dto.City;
        user.Pressure = dto.Pressure;
        user.AvgUpSystolicPressure = dto.AvgUpSystolicPressure;
        user.AvgDownSystolicPressure = dto.AvgDownSystolicPressure;
        user.AvgUpDialysticPressure = dto.AvgUpDialysticPressure;
        user.AvgDonwDialysticPressure = dto.AvgDonwDialysticPressure;
        await _userManager.UpdateAsync(user);
        var role = _userManager
               .GetRolesAsync(user).Result
               .FirstOrDefault()!;
        return new UserDto(user, role);
    }
}
