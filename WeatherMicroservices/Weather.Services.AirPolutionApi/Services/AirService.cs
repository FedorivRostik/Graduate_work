using Newtonsoft.Json;
using Weather.Services.AirPolutionApi.Dtos;
using Weather.Services.AirPolutionApi.Services.Interfaces;

namespace Weather.Services.AirPolutionApi.Services;

public class AirService: IAirService
{
    private readonly IHttpClientFactory _httpClientFactory;
    private readonly IConfiguration _configuration;

    public AirService(IHttpClientFactory httpClientFactory, IConfiguration configuration)
    {
        _httpClientFactory = httpClientFactory;
        _configuration = configuration;
    }

    public async Task<AirPollutionData> GetAirQuality(double latitude, double longitude)
    {
        var client = _httpClientFactory.CreateClient("Air");

        string apiUrl = $"http://api.openweathermap.org/data/2.5/air_pollution?lat={latitude}&lon={longitude}&appid={_configuration["AirKey"]}";
        client.BaseAddress = new Uri(apiUrl);
        var response = await client.GetAsync("");

        var apiContent = await response.Content.ReadAsStringAsync();
        var resp = JsonConvert.DeserializeObject<AirPollutionData>(apiContent);
        return resp;
    }
}
