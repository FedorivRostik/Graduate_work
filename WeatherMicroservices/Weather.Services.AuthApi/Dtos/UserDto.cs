using Weather.Services.AuthApi.Models;

namespace Weather.Services.AuthApi.Dtos;

public class UserDto
{
    public string UserId { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public string PhoneNumber { get; set; } = string.Empty;
    public string Role { get; set; } = string.Empty;

    public UserDto()
    {
        
    }

    public UserDto(ApplicationUser user,string role)
    {
        UserId = user.Id;
        Email = user.Email!;
        Name = user.Name;
        PhoneNumber = user.PhoneNumber!;
        Role = role;
    }
}
