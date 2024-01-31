using System.ComponentModel.DataAnnotations;
using Weather.Services.AuthApi.Utilities.Constants;

namespace Weather.Services.AuthApi.Dtos.Auths;

public class RegisterRequestDto
{
    [Required]
    [EmailAddress]
    public string Email { get; set; } = string.Empty;
    [Required]

    public string Name { get; set; } = string.Empty;
    [Phone]
    public string PhoneNumber { get; set; } = string.Empty;
    [Required]
    public string Password { get; set; } = string.Empty;
    public string? Role { get; set; } = AppRoles.CustomerRole;
}
