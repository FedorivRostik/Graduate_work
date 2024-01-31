using Microsoft.AspNetCore.Mvc;
using Weather.Services.ProductApi.Dtos;
using Weather.Services.ProductApi.Dtos.Extensions;
using Weather.Services.ProductApi.Dtos.Genres;
using Weather.Services.ProductApi.Services.Interfaces;

namespace Weather.Services.ProductApi.Controllers;

[Route("api/genres")]
[ApiController]
public class GenreControllerApi : ControllerBase
{
    private readonly IGenreService _genreService;
    private ResponseDto ResponseDto { get; set; } = new();

    public GenreControllerApi(IGenreService genreService)
    {
        _genreService = genreService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllAsync() 
    {
        var result = await _genreService.GetAllAsync();

        return Ok(ResponseDto.SetResult(result));
    }

    [HttpPost]
    public async Task<IActionResult> CreateAsync([FromBody] GenreCreateDto dto)
    {
        var result = await _genreService.CreateAsync(dto);

        return Ok(ResponseDto.SetResult(result));
    }
}
