using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Weather.Services.ProductApi.Data;
using Weather.Services.ProductApi.Dtos.Genres;
using Weather.Services.ProductApi.Models;
using Weather.Services.ProductApi.Services.Interfaces;

namespace Weather.Services.ProductApi.Services;

public class GenreService : IGenreService
{
    private readonly AppDbContext _db;
    private readonly IMapper _mapper;

    public GenreService(
        AppDbContext db,
        IMapper mapper)
    {
        _db = db;
        _mapper = mapper;
    }

    public async Task<IEnumerable<GenreDto>> GetAllAsync()
    {
        var genres = await _db.Genres.ToListAsync();

        return _mapper.Map<IEnumerable<GenreDto>>(genres);
    }

    public async Task<GenreDto> CreateAsync(GenreCreateDto dto)
    {
        var genre = _mapper.Map<Genre>(dto);
        
        genre = (await _db.Genres.AddAsync(genre)).Entity;
        await _db.SaveChangesAsync();
        
        return _mapper.Map<GenreDto>(genre);
    }
}
