using Microsoft.AspNetCore.Mvc;
using Weather.Services.AirPolutionApi.Dtos;
using Weather.Services.AirPolutionApi.Dtos.Extensions;
using Weather.Services.AirPolutionApi.Services.Interfaces;

namespace Weather.Services.AirPolutionApi.Controllers;

[Route("api/air")]
[ApiController]

public class AirController : ControllerBase
{
    private readonly IAirService _airService;
    private ResponseDto ResponseDto { get; set; } = new();

    public AirController(IAirService airService)
    {
        _airService = airService;
    }

    [ResponseCache(Duration = 86400)]
    [HttpGet("city")]
    public async Task<ActionResult<AirPollutionData>> GetAirQuality(double latitude, double longitude)
    {
        var result = await _airService.GetAirQuality(latitude, longitude);
        return Ok(ResponseDto.SetResult(result));
    }
}
