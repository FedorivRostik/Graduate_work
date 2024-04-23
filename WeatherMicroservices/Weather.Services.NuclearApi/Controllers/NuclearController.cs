using Microsoft.AspNetCore.Mvc;
using Weather.Services.NuclearApi.Dtos;
using Weather.Services.NuclearApi.Dtos.Extensions;
using Weather.Services.NuclearApi.Services.Interfaces;

namespace Weather.Services.NuclearApi.Controllers;

[Route("api/nuclears")]
[ApiController]
public class NuclearController : ControllerBase
{
    private readonly INuclearService _nuclearService;
    private ResponseDto ResponseDto { get; set; } = new();

    public NuclearController(INuclearService nuclearService)
    {
        _nuclearService = nuclearService;
    }

    [ResponseCache(Duration = 86400)]
    [HttpGet("city/{city}")]
    public async Task<ActionResult<ResponseDto>> GetValueByNameAsync([FromRoute] string city)
    {
       var result=await _nuclearService.GenerateCityAsync(city);
        return Ok(ResponseDto.SetResult(result));
    }

    [ResponseCache(Duration = 86400)]
    [HttpGet("disctrict/{disctrict}")]
    public async Task<ActionResult<ResponseDto>> GetValueByDisctrictAsync([FromRoute] string disctrict)
    {
        try
        {

        var result = await _nuclearService.GenerateDistrictAsync(disctrict);
        return Ok(ResponseDto.SetResult(result));
        }
        catch (Exception)
        {

            throw;
        }
    }
}
