using Microsoft.AspNetCore.Identity;

namespace Weather.Services.AuthApi.Models;

public class ApplicationUser : IdentityUser
{
    public string Name { get; set; } = string.Empty;
}

