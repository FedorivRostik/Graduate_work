using Weather.Services.ProductApi.Dtos.Genres;

namespace Weather.Services.ProductApi.Services.Interfaces;

public interface IGenreService
{
    Task<IEnumerable<GenreDto>> GetAllAsync();
    Task<GenreDto> CreateAsync(GenreCreateDto dto);
}
