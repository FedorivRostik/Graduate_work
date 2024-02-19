using Weather.Services.NuclearApi.Dtos;

namespace Weather.Services.NuclearApi.Services.Interfaces;

public interface INuclearService
{
    Task<NuclearResponseDto> GenerateCityAsync(string city);
    Task<NuclearResponseDto> GenerateDistrictAsync(string district);
}
