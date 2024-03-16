using Weather.Services.AirPolutionApi.Dtos;

namespace Weather.Services.AirPolutionApi.Services.Interfaces;

public interface IAirService
{
    public  Task<AirPollutionData> GetAirQuality(double latitude, double longitude);
}
