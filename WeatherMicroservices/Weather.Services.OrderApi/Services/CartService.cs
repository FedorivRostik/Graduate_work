using AutoMapper;
using Azure.Core;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Weather.Services.CartApi.Data;
using Weather.Services.CartApi.Dtos.CartDetails;
using Weather.Services.CartApi.Dtos.CartHeaders;
using Weather.Services.CartApi.Dtos.Carts;
using Weather.Services.CartApi.Dtos.LiqPay;
using Weather.Services.CartApi.Helpers;
using Weather.Services.CartApi.Models;
using Weather.Services.CartApi.Services.Interfaces;
using Weather.Services.CartApi.Utilities.Constants;

namespace Weather.Services.CartApi.Services;

public class CartService : ICartService
{
    private readonly AppDbContext _db;
    private readonly IMapper _mapper;
    private readonly IProductService _productService;
    private readonly IHttpClientFactory _clientFactory;
    private readonly LiqPayHelper _liqPayHelper;

    public CartService(AppDbContext db,
        IMapper mapper,
        IProductService productService,
        IHttpClientFactory clientFactory,
        LiqPayHelper liqPayHelper)
    {
        _db = db;
        _mapper = mapper;
        _productService = productService;
        _clientFactory = clientFactory;
        _liqPayHelper = liqPayHelper;
    }

    public async Task<bool> CartUpdateShippmentInfoAsync(HeaderUpdateShippmentInfoDto cartUpdateShippmentInfoDto)
    {
        var headerId = Guid.Parse(cartUpdateShippmentInfoDto.CartHeaderId);
        var header = await _db.CartHeaders.AsNoTracking().FirstAsync(x => x.CartHeaderId == headerId);

        header.UpdateShippingInfo(cartUpdateShippmentInfoDto.Address, cartUpdateShippmentInfoDto.Email, cartUpdateShippmentInfoDto.Phone);

        _db.Update(header);
        await _db.SaveChangesAsync();
        return true;
    }

    public async Task<bool> CheckPayedCartStatusAsync(string cartHeaderId)
    {

        var liqPayModel = _liqPayHelper.GetLiqPayModelStatus(cartHeaderId);
        HttpClient client = _clientFactory.CreateClient("LiqPay");
        HttpRequestMessage message = new HttpRequestMessage(HttpMethod.Post, "https://www.liqpay.ua/api/request");

        var data = new Dictionary<string, string>
        {
            { "data", liqPayModel.Data },
            { "signature", liqPayModel.Signature }
        };

        message.Content = new FormUrlEncodedContent(data);

        HttpResponseMessage response = await client.SendAsync(message);

        var apiContent = await response.Content.ReadAsStringAsync();
        var responseDto = JsonConvert.DeserializeObject<LiqPayResponse>(apiContent);

        if (responseDto.status == "success")
        {
            var orderHeaderResult = await _db.CartHeaders.FirstAsync(x => x.CartHeaderId.ToString() == cartHeaderId);
            if (orderHeaderResult is not null)
            {
                orderHeaderResult.Status = CartStatuses.StatusPayed;
                _db.Update(orderHeaderResult);
                await _db.SaveChangesAsync();

                return true;
            }

        }

        return false;
    }

    public async Task<bool> DeleteDetailsAsync(string cartDetailsId)
    {
        var detail = await _db.CartDetails
             .AsNoTracking()
             .FirstAsync(x => x.CartDetailsId.ToString() == cartDetailsId);

        _db.Remove(detail);
        _db.SaveChanges();
        return true;
    }

    public async Task<CartResponseDto> GetCartAsync(string userId)
    {
        var products = await _productService.GetProducts();

        CartResponseDto cart = new()
        {
            CartHeader = _mapper.Map<CartHeaderDto>(await _db.CartHeaders.FirstOrDefaultAsync(u => u.UserId == userId.ToString()
            && u.Status == CartStatuses.StatusOpen)),
        };
        cart.CartDetails = _mapper.Map<IEnumerable<CartDetailsDto>>(await _db.CartDetails
                .Where(u => u.CartHeaderId.ToString() == cart.CartHeader.CartHeaderId).ToListAsync());

        foreach (var item in cart.CartDetails)
        {
            var product = products.FirstOrDefault(x => x.ProductId.ToString() == item.ProductId);
            item.ProductDto = product;
            item.Price = product.Price;
            item.Discount = product.Discount;
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

    public async Task<CartHeaderDto> GetCartHeaderAsync(string headerId)
    {
        return _mapper.Map<CartHeaderDto>(await _db.CartHeaders.FirstOrDefaultAsync(u => u.CartHeaderId.ToString() == headerId));
    }

    public async Task<IEnumerable<CartResponseDto>> GetUserOrders(string userId)
    {
        var products = await _productService.GetProducts();

        var userCartHeaders = await _db.CartHeaders
            .Where(h => h.UserId == userId)
            .ToListAsync();

        var cartResponses = new List<CartResponseDto>();

        foreach (var h in userCartHeaders)
        {
            var cartHeader = _mapper.Map<CartHeaderDto>(h);
            var cartDetails = _mapper.Map<IEnumerable<CartDetailsDto>>(
                await _db.CartDetails
                    .Where(u => u.CartHeaderId.ToString() == cartHeader.CartHeaderId)
                    .ToListAsync()
            );

            foreach (var item in cartDetails)
            {
                var product = products.FirstOrDefault(x => x.ProductId.ToString() == item.ProductId);
                item.ProductDto = product;
                item.Price = product.Price;
                item.Discount = product.Discount;
                cartHeader.CartTotal += (double)(item.Count * item.ProductDto.Price);
            }

            cartResponses.Add(new CartResponseDto
            {
                CartHeader = cartHeader,
                CartDetails = cartDetails
            });
        }

        return cartResponses;

    }

    public async Task<bool> UpdateCartHeaderStatusAsync(CartUpdateHeaderStatusDto cartUpdateHeaderStatusDto)
    {
        var cartHeaderId = Guid.Parse(cartUpdateHeaderStatusDto.CartHeaderId);
        var cartHeader = await _db.CartHeaders.FirstOrDefaultAsync(x => x.CartHeaderId == cartHeaderId);

        cartHeader.Status = cartUpdateHeaderStatusDto.Status;
        _db.Update(cartHeader);
        await _db.SaveChangesAsync();
        return true;
    }

    public async Task<CartUpdateDetailsDto> UpdateDetailsAsync(CartUpdateDetailsDto cartUpdateDetailsDto)
    {
        await _db.CartDetails
           .AsNoTracking()
           .FirstAsync(x => x.CartDetailsId.ToString() == cartUpdateDetailsDto.CartDetailsId);

        var updatedDetail = _mapper.Map<CartDetail>(cartUpdateDetailsDto);

        if (updatedDetail.Count <= 0)
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
           .FirstOrDefaultAsync(u => u.UserId == cartDto.CartHeader.UserId
           && u.Status == CartStatuses.StatusOpen);

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
