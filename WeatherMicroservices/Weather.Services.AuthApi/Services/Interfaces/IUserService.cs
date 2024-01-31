using Weather.Services.AuthApi.Dtos;

namespace Weather.Services.AuthApi.Services.Interfaces;

public interface IUserService
{
    Task<List<UserDto>> GetAllUsersAsync();
}
