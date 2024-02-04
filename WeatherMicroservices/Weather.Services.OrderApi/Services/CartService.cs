using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Store.Services.CartApi.Dto;
using Weather.Services.CartApi.Data;
using Weather.Services.CartApi.Models;
using Weather.Services.CartApi.Services.Interfaces;

namespace Weather.Services.CartApi.Services;

public class CartService : ICartService
{
    private readonly AppDbContext _db;
    private readonly IMapper _mapper;


    public CartService(AppDbContext db,
        IMapper mapper)
    {
        _db = db;
        _mapper = mapper;
    }

    public async Task<bool> UpsertCart(CartAddDto cartDto)
    {
        var cartHeaderFromDb = await _db.CartHeaders
           .AsNoTracking()
           .FirstOrDefaultAsync(u => u.UserId == cartDto.CartHeader.UserId);

        if (cartHeaderFromDb == null)
        {
            var header = _mapper.Map<CartHeader>(cartDto.CartHeader);
            await _db.CartHeaders.AddAsync(header);
            await _db.SaveChangesAsync();
            cartDto.CartDetails.CartHeaderId = header.CartHeaderId.ToString();
            await _db.CartDetails.AddAsync(_mapper.Map<CartDetail>(cartDto.CartDetails));
            await _db.SaveChangesAsync();
            return true;
        }

        var cartdetails = await _db.CartDetails
            .AsNoTracking()
            .FirstOrDefaultAsync(cardDetail => cardDetail.ProductId.ToString() == cartDto.CartDetails.ProductId
                && cardDetail.CartHeaderId == cartHeaderFromDb.CartHeaderId);

        if (cartdetails == null)
        {
            cartDto.CartDetails.CartHeaderId = cartHeaderFromDb.CartHeaderId.ToString();
            await _db.CartDetails.AddAsync(_mapper.Map<CartDetail>(cartDto.CartDetails));
            await _db.SaveChangesAsync();
            return true;
        }

        cartDto.CartDetails.Count += cartdetails.Count;
        cartDto.CartDetails.CartHeaderId = cartdetails.CartHeaderId.ToString();
        cartDto.CartDetails.CartDetailsId = cartdetails.CartDetailsId.ToString();
        _db.CartDetails.Update(_mapper.Map<CartDetail>(cartDto.CartDetails));
        await _db.SaveChangesAsync();
        return true;
    }
}
