using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Weather.Services.CartApi.Data;
using Weather.Services.CartApi.Dtos.CartDetails;
using Weather.Services.CartApi.Dtos.CartHeaders;
using Weather.Services.CartApi.Dtos.Carts;
using Weather.Services.CartApi.Models;
using Weather.Services.CartApi.Services.Interfaces;

namespace Weather.Services.CartApi.Services;

public class CartService : ICartService
{
    private readonly AppDbContext _db;
    private readonly IMapper _mapper;
    private readonly IProductService _productService;


    public CartService(AppDbContext db,
        IMapper mapper,
        IProductService productService)
    {
        _db = db;
        _mapper = mapper;
        _productService = productService;
    }

    public async Task<bool> DeleteDetailsAsync(string cartDetailsId)
    {
      var detail =  await _db.CartDetails
           .AsNoTracking()
           .FirstAsync(x => x.CartDetailsId.ToString() == cartDetailsId);

        _db.Remove(detail);
        _db.SaveChanges();
        return true;
    }

    public async Task<CartResponseDto> GetCartAsync(string userId)
    {
        CartResponseDto cart = new()
        {
            CartHeader = _mapper.Map<CartHeaderDto>(await _db.CartHeaders.FirstOrDefaultAsync(u => u.UserId == userId.ToString())),
        };
        cart.CartDetails = _mapper.Map<IEnumerable<CartDetailsDto>>(await _db.CartDetails
                .Where(u => u.CartHeaderId.ToString() == cart.CartHeader.CartHeaderId).ToListAsync());

        var products = await _productService.GetProducts();
        foreach (var item in cart.CartDetails)
        {
            var product = products.FirstOrDefault(x => x.ProductId.ToString() == item.ProductId);
            item.ProductDto = product;
            item.Price = product.Price;
            item.Discount= product.Discount;
            cart.CartHeader.CartTotal += (double)(item.Count * item.ProductDto.Price);
        }

        //if (!string.IsNullOrEmpty(cart.CartHeader.CouponCode))
        //{
        //    var coupon = await _couponService.GetCoupon(cart.CartHeader.CouponCode);
        //    if (coupon != null && cart.CartHeader.CartTotal > coupon.MinAmount)
        //    {
        //        cart.CartHeader.CartTotal -= coupon.DiscountAmount;
        //        cart.CartHeader.Discount = coupon.DiscountAmount;
        //    }
        //}
        return cart;
    }

    public async Task<CartUpdateDetailsDto> UpdateDetailsAsync(CartUpdateDetailsDto cartUpdateDetailsDto)
    {
         await _db.CartDetails
            .AsNoTracking()
            .FirstAsync(x => x.CartDetailsId.ToString() == cartUpdateDetailsDto.CartDetailsId);

        var updatedDetail = _mapper.Map<CartDetail>(cartUpdateDetailsDto);

        if (updatedDetail.Count<=0)
        {
            throw new ArgumentException("Amount of product cannot be smaller or equal to 0");
        }

        _db.CartDetails.Update(updatedDetail);
        await _db.SaveChangesAsync();

        return _mapper.Map<CartUpdateDetailsDto>(updatedDetail);
    }

    public async Task<bool> UpsertCartAsync(CartAddDto cartDto)
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
