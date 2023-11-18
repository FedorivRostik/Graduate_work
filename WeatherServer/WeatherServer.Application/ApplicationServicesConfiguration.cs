using Microsoft.Extensions.DependencyInjection;
using WeatherServer.Application.Interfaces.Services;
using WeatherServer.Application.Services;

namespace WeatherServer.Application;
public static class ApplicationServicesConfiguration
{
	public static void AddApplicationServices(this IServiceCollection services)
	{
		services.AddTransient<IAuthService, AuthService>();
	}
}