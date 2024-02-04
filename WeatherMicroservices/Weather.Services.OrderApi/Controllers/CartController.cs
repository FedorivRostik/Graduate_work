using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Weather.Services.CartApi.Dtos;
using Weather.Services.CartApi.Dtos.Extensions;
using Weather.Services.CartApi.Models;
using Weather.Services.CartApi.Data;
using Weather.Services.CartApi.Services.Interfaces;
using Weather.Services.CartApi.Dtos.Carts;

namespace Weather.Services.CartApi.Controllers;
[Route("api/carts")]
[ApiController]
public class CartController : ControllerBase
{
    private readonly ICartService _cartService;
    private ResponseDto ResponseDto { get; set; } = new();
    public CartController(
        ICartService cartService)
    {
        _cartService = cartService;
    }

    [HttpPost("cartUpsert")]
    public async Task<ActionResult<ResponseDto>> CartUpsertAsync(CartAddDto cartDto)
    {
        var result = await _cartService.UpsertCartAsync(cartDto);
        return Ok(ResponseDto.SetResult(result));
    }

    [HttpGet("getCart/{userId}")]
    public async Task<ActionResult<CartResponseDto>> GetUserCartAsync([FromRoute] string userId)
    {
        var result = await _cartService.GetCartAsync(userId);
        return Ok(ResponseDto.SetResult(result));
    }
}
