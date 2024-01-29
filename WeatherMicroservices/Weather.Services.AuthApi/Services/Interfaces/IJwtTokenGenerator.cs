using Weather.Services.AuthApi.Models;

namespace Weather.Services.AuthApi.Services.Interfaces;

public interface IJwtTokenGenerator
{
    string GenerateToken(ApplicationUser user, IEnumerable<string> roles);
}