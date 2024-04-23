using Weather.Services.AuthApi.Enums;
using Weather.Services.AuthApi.Models;

namespace Weather.Services.AuthApi.Dtos;

public class UserDto
{
    public string UserId { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public string PhoneNumber { get; set; } = string.Empty;
    public string Role { get; set; } = string.Empty;


    public double Weight { get; set; }
    public double Age { get; set; }
    public string Region { get; set; } = string.Empty;
    public string City { get; set; } = string.Empty;
    public Pressure Pressure { get; set; } = Pressure.None;
    public double AvgUpSystolicPressure { get; set; }
    public double AvgDownSystolicPressure { get; set; }
    public double AvgUpDialysticPressure { get; set; }
    public double AvgDonwDialysticPressure { get; set; }
    public UserDto()
    {

    }

    public UserDto(ApplicationUser user)
    {
        UserId = user.Id;
        Email = user.Email!;
        Name = user.Name;
        PhoneNumber = user.PhoneNumber!;
        Weight = user.Weight;
        Age = user.Age;
        Region = user.Region;
        City = user.City;
        Pressure = user.Pressure;
        AvgUpSystolicPressure = user.AvgUpSystolicPressure;
        AvgDownSystolicPressure = user.AvgDownSystolicPressure;
        AvgUpDialysticPressure = user.AvgUpDialysticPressure;
        AvgDonwDialysticPressure = user.AvgDonwDialysticPressure;
    }

    public UserDto(ApplicationUser user, string role) : this(user)
    {
        Role = role;
    }
}
