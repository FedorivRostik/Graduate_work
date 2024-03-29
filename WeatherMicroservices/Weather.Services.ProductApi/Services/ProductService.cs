﻿using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Weather.Services.ProductApi.Data;
using Weather.Services.ProductApi.Data.Repositories.Interfaces;
using Weather.Services.ProductApi.Dtos.Products;
using Weather.Services.ProductApi.Models;
using Weather.Services.ProductApi.Services.Interfaces;

namespace Weather.Services.ProductApi.Services;

public class ProductService : IProductService
{
    private readonly AppDbContext _db;
    private readonly IMapper _mapper;
    private readonly IPictureRepository _pictureRepository;

    public ProductService(
        AppDbContext db,
        IMapper mapper,
        IPictureRepository pictureRepository)
    {
        _db = db;
        _mapper = mapper;
        _pictureRepository = pictureRepository;
    }


    public async Task<ProductDto> AddProductAsync(ProductCreateDto productDto)
    {
        await CheckGenre(productDto);

        if (!string.IsNullOrEmpty(productDto.ImageUrl))
        {
            productDto.ImageUrl = await _pictureRepository.UploadAsync(productDto.ImageUrl, productDto.Slug);
        }

        var product = _mapper.Map<Product>(productDto);
        var result = (await _db.Products.AddAsync(product)).Entity;
        await _db.SaveChangesAsync();

        return _mapper.Map<ProductDto>(result);
    }

    public async Task<ProductDto> DeleteByIdSlugProductAsync(Guid id)
    {
        var result = await _db.Products.FirstOrDefaultAsync(p => p.ProductId == id);

        if (result is null)
        {
            throw new InvalidOperationException("cannot find");
        }

        _db.Products.Remove(result);
        await _db.SaveChangesAsync();
        return _mapper.Map<ProductDto>(result);
    }

    public async Task<IEnumerable< ProductDto>> GetRandomTwoByCategory(string gerneName)
    {
        var result = await _db.Products
            .Include(x=>x.Genre)
            .Where(p => p.Genre!.Name.ToLower()==gerneName.ToLower())
            .OrderBy(x => x.ProductId)
            .Take(2)
            .ToListAsync();

        if (result is null)
        {
            throw new InvalidOperationException("cannot find");
        }

        await _db.SaveChangesAsync();
        return _mapper.Map<IEnumerable<ProductDto>>(result);
    }

    public async Task<IEnumerable<ProductDto>> GetAllProductAsync()
    {
        var result = await _db.Products.Include(x => x.Genre)
            .AsNoTracking()
            .ToListAsync();

        return _mapper.Map<IEnumerable<ProductDto>>(result);
    }

    public async Task<ProductDto> GetByIdProductAsync(Guid id)
    {
        var result = await _db.Products.FirstOrDefaultAsync(p => p.ProductId == id);

        if (result is null)
        {
            throw new InvalidOperationException("cannot find");
        }

        return _mapper.Map<ProductDto>(result);
    }

    public async Task<ProductDto> GetBySlugProductAsync(string slug)
    {
        var result = await _db.Products.FirstOrDefaultAsync(p => p.Slug.ToLower() == slug.ToLower());

        if (result is null)
        {
            throw new InvalidOperationException("cannot find");
        }

        return _mapper.Map<ProductDto>(result);
    }

    public async Task<ProductDto> UpdateProductAsync(ProductDto productDto)
    {
        var result = await _db.Products.AsNoTracking().FirstOrDefaultAsync(p => p.ProductId == productDto.ProductId);

        if (result is null)
        {
            throw new InvalidOperationException("cannot find");
        }

        var product = _mapper.Map<Product>(productDto);

        product = _db.Products?.Update(product)!.Entity;
        await _db.SaveChangesAsync();
        return _mapper.Map<ProductDto>(result);
    }

    private async Task CheckGenre(ProductCreateDto productDto)
    {
        if (!string.IsNullOrEmpty(productDto.GenreId))
        {
            var genreId = Guid.Parse(productDto.GenreId);
            var genre = await _db.Genres.FirstOrDefaultAsync(x => x.GenreId == genreId);
            if (genre is null)
            {
                productDto.GenreId = default!;
            }
        }
    }
}
